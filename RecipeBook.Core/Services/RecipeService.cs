using Microsoft.EntityFrameworkCore;

using RecipeBook.Data;
using RecipeBook.Data.Models;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Recipe;
using RecipeBook.Core.Models.Recipe.Enums;

namespace RecipeBook.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeBookDbContext dbContext;

        public RecipeService(RecipeBookDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<AllRecipesViewModel>> AllRecipesAsync(AllRecipesQueryModel queryModel)
        {
            IQueryable<Recipe> recipesQuery = dbContext
                .Recipes
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                recipesQuery = recipesQuery
                    .Where(r => r.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string pattern = $"%{queryModel.SearchString.ToLower()}%";

                recipesQuery = recipesQuery
                    .Where(r => EF.Functions.Like(r.Name, pattern) || 
                                EF.Functions.Like(r.Steps, pattern) ||
                                EF.Functions.Like(r.Products, pattern));
            }

            recipesQuery = queryModel.RecipeSorting switch
            {
                RecipeSorting.Newest => recipesQuery
                    .OrderByDescending(r => r.CreatedOn),
                RecipeSorting.Oldest => recipesQuery
                    .OrderBy(r => r.CreatedOn),
                _ => recipesQuery
                    .OrderByDescending(r => r.CreatedOn)
            };

            return await recipesQuery
                .Where(r => r.IsActive)
                .Select(r => new AllRecipesViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    ImageUrl = r.ImageUrl
                })
                .ToListAsync();
        }

        public async Task AddRecipeAsync(RecipeFormModel model, Guid chefId)
        {
            var recipe = new Recipe()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Products = model.Products,
                Steps = model.Steps,
                Servings = model.Servings,
                CookingTime = model.CookingTime,
                CategoryId = model.CategoryId,
                ChefId = chefId,
            };

            await dbContext.Recipes.AddAsync(recipe);
            await dbContext.SaveChangesAsync();
        }

        public async Task<DetailsRecipeViewModel> RecipeDetailsAsync(int id)
        {
            return await dbContext.Recipes
                .Where(r => r.Id == id && r.IsActive)
                .Select(r => new DetailsRecipeViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Category = r.Category.Name,
                    ImageUrl = r.ImageUrl,
                    Products = r.Products,
                    Steps = r.Steps,
                    Servings = r.Servings,
                    CookingTime = r.CookingTime,
                    Chef = r.Chef.Name,
                    ChefId = r.ChefId
                })
                .FirstAsync();
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await dbContext.Recipes
                .Where(r => r.IsActive)
                .AnyAsync(r => r.Id == id);
        }

        public async Task<RecipeFormModel> RecipeForEditByIdAsync(int id)
        {
            return await dbContext.Recipes

                .Where(r => r.Id == id && r.IsActive)
                .Select(r => new RecipeFormModel()
                {
                    Name = r.Name,
                    CategoryId = r.CategoryId,
                    ImageUrl = r.ImageUrl,
                    Products = r.Products,
                    Steps = r.Steps,
                    Servings = r.Servings,
                    CookingTime = r.CookingTime
                })
                .FirstAsync();
        }

        public async Task<bool> IsChefWithIdOwnerOfRecipeWithIdAsync(Guid chefId, int recipeId)
        {
            return await dbContext.Recipes
                .Where(r => r.IsActive)
                .AnyAsync(r => r.Id == recipeId && r.ChefId == chefId);
        }

        public async Task EditRecipeAsync(int id, RecipeFormModel model)
        {
            var recipe = await dbContext.Recipes
                .Where(r => r.IsActive)
                .FirstAsync(r => r.Id == id);

            recipe.Name = model.Name;
            recipe.ImageUrl = model.ImageUrl;
            recipe.Products = model.Products;
            recipe.Steps = model.Steps;
            recipe.Servings = model.Servings;
            recipe.CategoryId = model.CategoryId;
            recipe.CookingTime = model.CookingTime;

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<AllRecipesViewModel>> MineRecipesAsync(Guid chefId)
        {
            return await dbContext.Recipes
                .Where(r => r.ChefId == chefId && r.IsActive)
                .Select(r => new AllRecipesViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    ImageUrl = r.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<List<AllRecipesViewModel>> SavedRecipesAsync(Guid userId)
        {
            return await dbContext.ApplicationUsersRecipes
                .Where(aur => aur.UserId == userId && aur.Recipe.IsActive)
                .Select(aur => new AllRecipesViewModel()
                {
                    Id = aur.RecipeId,
                    Name = aur.Recipe.Name,
                    ImageUrl = aur.Recipe.ImageUrl
                })
                .ToListAsync();
        }

        public async Task SaveRecipe(Guid userId, int recipeId)
        {
            ApplicationUserRecipe applicationUserRecipe = new ApplicationUserRecipe()
            {
                UserId = userId,
                RecipeId = recipeId
            };

            await dbContext.ApplicationUsersRecipes.AddAsync(applicationUserRecipe);
            await dbContext.SaveChangesAsync();
        }

        public async Task UnsaveRecipe(Guid userId, int recipeId)
        {
            var applicationUserRecipe = await dbContext.ApplicationUsersRecipes
                .Where(aur => aur.UserId == userId && aur.RecipeId == recipeId)
                .FirstAsync();

            dbContext.ApplicationUsersRecipes.Remove(applicationUserRecipe);
            await dbContext.SaveChangesAsync();
        }

        public async Task<DeleteRecipeViewModel> GetRecipeForDeleteAsync(int id)
        {
            return await dbContext.Recipes
                .Where(r => r.IsActive && r.Id == id)
                .Select(r => new DeleteRecipeViewModel()
                {
                    Name = r.Name,
                    Category = r.Category.Name,
                    ImageUrl = r.ImageUrl
                })
                .FirstAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await dbContext.Recipes
                .Where(r => r.IsActive && r.Id == id)
                .FirstAsync();

            recipe.IsActive = false;

            await dbContext.SaveChangesAsync();
        }
    }
}

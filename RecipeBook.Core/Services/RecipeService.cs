using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Recipe;
using RecipeBook.Core.Models.Recipe.Enums;
using RecipeBook.Data;
using RecipeBook.Data.Models;

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
                //.Where(r => r.IsActive)
                .Select(r => new AllRecipesViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    ImageUrl = r.ImageUrl
                })
                .ToListAsync();
        }

        public async Task AddRecipeAsync(AddRecipeFormModel model, Guid chefId)
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

            dbContext.Recipes.Add(recipe);
            await dbContext.SaveChangesAsync();
        }

        public async Task<DetailsRecipeViewModel> RecipeDetailsAsync(int id)
        {
            return await dbContext.Recipes
                .Where(r => r.Id == id)
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
    }
}

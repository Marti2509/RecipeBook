using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Recipe;
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
    }
}

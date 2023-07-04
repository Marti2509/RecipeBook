using RecipeBook.Core.Models.Recipe;

namespace RecipeBook.Core.Contracts
{
    public interface IRecipeService
    {
        public Task AddRecipeAsync(AddRecipeFormModel model, Guid chefId);
    }
}

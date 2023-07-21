using RecipeBook.Core.Models.Recipe;

namespace RecipeBook.Core.Contracts
{
    public interface IRecipeService
    {
        public Task AddRecipeAsync(AddRecipeFormModel model, Guid chefId);

        public Task<List<AllRecipesViewModel>> AllRecipesAsync(AllRecipesQueryModel queryModel);

        public Task<DetailsRecipeViewModel> RecipeDetailsAsync(int id);
    }
}

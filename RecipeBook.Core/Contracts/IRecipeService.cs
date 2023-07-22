using RecipeBook.Core.Models.Recipe;

namespace RecipeBook.Core.Contracts
{
    public interface IRecipeService
    {
        public Task AddRecipeAsync(RecipeFormModel model, Guid chefId);

        public Task<List<AllRecipesViewModel>> AllRecipesAsync(AllRecipesQueryModel queryModel);

        public Task<DetailsRecipeViewModel> RecipeDetailsAsync(int id);

        public Task<bool> ExistsByIdAsync(int id);

        public Task<RecipeFormModel> RecipeForEditByIdAsync(int id);

        public Task<bool> IsChefWithIdOwnerOfRecipeWithIdAsync(Guid chefId, int recipeId);

        public Task EditRecipeAsync(int id, RecipeFormModel model);
    }
}

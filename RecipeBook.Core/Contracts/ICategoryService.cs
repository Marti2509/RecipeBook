using RecipeBook.Core.Models.Category;

namespace RecipeBook.Core.Contracts
{
    public interface ICategoryService
    {
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        public Task<bool> CategoryExistsByIdAsync(int id);

        public Task<List<string>> GetAllCategoryNamesAsync();

        public Task<bool> CategoryExistsByNameAsync(string name);

        public Task AddCategoryAsync(CategoryFormModel model);

        public Task<CategoryFormModel> GetCategoryByIdAsync(int id);

        public Task EditCategoryAsync(int id, CategoryFormModel model);

        public Task DeleteCategory(int id);
    }
}

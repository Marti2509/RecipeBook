using RecipeBook.Core.Models.Category;

namespace RecipeBook.Core.Contracts
{
    public interface ICategoryService
    {
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        public Task<bool> CategoryExistsByIdAsync(int id);

        public Task<List<string>> GetAllCategoryNamesAsync();
    }
}

using Microsoft.EntityFrameworkCore;

using RecipeBook.Data;
using RecipeBook.Data.Models;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Category;

namespace RecipeBook.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly RecipeBookDbContext dbContext;

        public CategoryService(RecipeBookDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories
                .Where(c => c.IsActive)
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            return await dbContext.Categories
                .AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<List<string>> GetAllCategoryNamesAsync()
        {
            return await dbContext.Categories
                .Where(c => c.IsActive)
                .Select(c => c.Name)
                .ToListAsync();
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            return await dbContext.Categories
                .AnyAsync(c => c.Name == name && c.IsActive);
        }

        public async Task AddCategoryAsync(CategoryFormModel model)
        {
            Category category = new Category()
            {
                Name = model.Name
            };

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<CategoryFormModel> GetCategoryByIdAsync(int id)
        {
            return await dbContext.Categories
                .Where(c => c.Id == id && c.IsActive)
                .Select(c => new CategoryFormModel()
                {
                    Name = c.Name
                })
                .FirstAsync();
        }

        public async Task EditCategoryAsync(int id, CategoryFormModel model)
        {
            Category category = await dbContext.Categories
                .Where(c => c.Id == id && c.IsActive)
                .FirstAsync();

            category.Name = model.Name;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var recipesWithCategory = await dbContext.Categories
                .Where(c => c.Id == id && c.IsActive)
                .Select(c => c.Recipes
                    .Where(r => r.IsActive))
                .FirstAsync();

            foreach (var recipe in recipesWithCategory)
            {
                recipe.IsActive = false;
            }

            var category = await dbContext.Categories
                .Where(c => c.Id == id)
                .FirstAsync();

            category.IsActive = false;

            await dbContext.SaveChangesAsync();
        }
    }
}

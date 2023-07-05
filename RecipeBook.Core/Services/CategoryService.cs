using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Category;
using RecipeBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .AnyAsync(c => c.Id == id);
        }
    }
}

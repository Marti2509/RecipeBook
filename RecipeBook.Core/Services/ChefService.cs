using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Data;
using RecipeBook.Data.Models;

namespace RecipeBook.Core.Services
{
    public class ChefService : IChefService
    {
        private readonly RecipeBookDbContext dbContext;

        public ChefService(RecipeBookDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Guid> GetChefIdByUserIdAsync(Guid userId)
        {
            return await dbContext.Chefs
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();
        }

        public async Task<bool> ChefExistsByUserIdAsync(Guid userId)
        {
            return await dbContext.Chefs
                .AnyAsync(c => c.UserId == userId);
        }
    }
}

using Microsoft.EntityFrameworkCore;

using RecipeBook.Data;
using RecipeBook.Data.Models;
using RecipeBook.Core.Contracts;

namespace RecipeBook.Core.Services
{
    public class ChefService : IChefService
    {
        private readonly RecipeBookDbContext dbContext;

        public ChefService(RecipeBookDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Guid> GetChefIdByUserIdAsync(Guid? userId)
        {
            return await dbContext.Chefs
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();
        }

        public async Task<bool> ChefExistsByUserIdAsync(Guid? userId)
        {
            return await dbContext.Chefs
                .AnyAsync(c => c.UserId == userId);
        }

        public async Task<bool> ChefExistsByNameAsync(string name)
        {
            return await dbContext.Chefs
                .AnyAsync(c => c.Name == name);
        }

        public async Task<bool> ChefExistsByPhoneNumberAsync(string phoneNumber)
        {
            return await dbContext.Chefs
                .AnyAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task Create(Guid userId, string name, string phoneNumber)
        {
            Chef chef = new Chef()
            {
                UserId = userId,
                Name = name,
                PhoneNumber = phoneNumber,
            };

            await dbContext.Chefs.AddAsync(chef);
            await dbContext.SaveChangesAsync();
        }
    }
}

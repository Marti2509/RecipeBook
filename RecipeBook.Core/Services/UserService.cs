using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Data;

namespace RecipeBook.Core.Services
{
    public class UserService : IUserService
    {
        private readonly RecipeBookDbContext dbContext;

        public UserService(RecipeBookDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<string> GetFullNameAsync(string email)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}

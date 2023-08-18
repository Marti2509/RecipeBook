using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.User;
using RecipeBook.Data;
using RecipeBook.Data.Models;

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

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            ApplicationUser? user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
            List<UserViewModel> allUsers = await dbContext
                .Users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();

            foreach (UserViewModel user in allUsers)
            {
                Chef? chef = dbContext
                    .Chefs
                    .FirstOrDefault(c => c.UserId.ToString() == user.Id);

                if (chef != null)
                {
                    user.PhoneNumber = chef.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber = string.Empty;
                }
            }

            return allUsers;
        }
    }
}

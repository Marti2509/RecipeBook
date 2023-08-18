using RecipeBook.Core.Models.User;

namespace RecipeBook.Core.Contracts
{
    public interface IUserService
    {
        public Task<string> GetFullNameAsync(string email);

        Task<string> GetFullNameByIdAsync(string userId);

        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}

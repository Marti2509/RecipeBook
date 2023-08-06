namespace RecipeBook.Core.Contracts
{
    public interface IUserService
    {
        public Task<string> GetFullNameAsync(string email);
    }
}

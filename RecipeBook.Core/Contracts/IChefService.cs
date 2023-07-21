namespace RecipeBook.Core.Contracts
{
    public interface IChefService
    {
        public Task<Guid> GetChefIdByUserIdAsync(Guid userId);

        public Task<bool> ChefExistsByUserIdAsync(Guid userId);

        public Task<bool> ChefExistsByNameAsync(string name);

        public Task<bool> ChefExistsByPhoneNumberAsync(string phoneNumber);

        public Task Create(Guid userId, string name, string phoneNumber);
    }
}

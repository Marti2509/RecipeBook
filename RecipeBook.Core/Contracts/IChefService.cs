namespace RecipeBook.Core.Contracts
{
    public interface IChefService
    {
        public Task<Guid> GetChefIdByUserIdAsync(Guid userId);

        public Task<bool> ChefExistsByUserIdAsync(Guid userId);
    }
}

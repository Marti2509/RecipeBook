using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Services;
using RecipeBook.Data;

using static RecipeBook.Core.Tests.SeedDatabase;

namespace RecipeBook.Core.Tests
{
    public class RecipeServiceTests
    {
        private DbContextOptions<RecipeBookDbContext> dbOptions;
        private RecipeBookDbContext dbContext;

        private IRecipeService recipeService;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RecipeBookDbContext>()
                .UseInMemoryDatabase("RecipeBookInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RecipeBookDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            await SeedDb(this.dbContext);

            this.recipeService = new RecipeService(this.dbContext);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueIfExists()
        {
            int id = Recipe1.Id;

            var result = await this.recipeService.ExistsByIdAsync(id);

            Assert.That(result);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseIfDoesNotExists()
        {
            int id = 123123;

            var result = await this.recipeService.ExistsByIdAsync(id);

            Assert.That(result == false);
        }

        [Test]
        public async Task IsChefWithIdOwnerOfRecipeWithIdAsyncShouldReturnTrueIfIs()
        {
            Guid chefId = Chef.Id;
            int recipeId = Recipe1.Id;

            var result = await this.recipeService.IsChefWithIdOwnerOfRecipeWithIdAsync(chefId, recipeId);

            Assert.That(result);
        }

        [Test]
        public async Task IsChefWithIdOwnerOfRecipeWithIdAsyncShouldReturnFalseIfIsNot()
        {
            Guid chefId = Guid.NewGuid();
            int recipeId = 123123;

            var result = await this.recipeService.IsChefWithIdOwnerOfRecipeWithIdAsync(chefId, recipeId);

            Assert.That(result == false);
        }

        [Test]
        public async Task IsRecipeSavedAsyncShouldReturnTrueIfIs()
        {
            Guid userId = User.Id;
            int recipeId = Recipe1.Id;

            var result = await this.recipeService.IsRecipeSavedAsync(userId, recipeId);

            Assert.That(result);
        }

        [Test]
        public async Task IsRecipeSavedAsyncShouldReturnFalseIfIsNot()
        {
            Guid userId = Guid.NewGuid();
            int recipeId = 123123;

            var result = await this.recipeService.IsRecipeSavedAsync(userId, recipeId);

            Assert.That(result == false);
        }
    }
}

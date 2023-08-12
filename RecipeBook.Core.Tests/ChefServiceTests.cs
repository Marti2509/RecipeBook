using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Services;
using RecipeBook.Data;

using static RecipeBook.Core.Tests.SeedDatabase;

namespace RecipeBook.Core.Tests
{
    public class ChefServiceTests
    {
        private DbContextOptions<RecipeBookDbContext> dbOptions;
        private RecipeBookDbContext dbContext;

        private IChefService chefService;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RecipeBookDbContext>()
                .UseInMemoryDatabase("RecipeBookInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RecipeBookDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            await SeedDb(this.dbContext);

            this.chefService = new ChefService(this.dbContext);
        }

        [Test]
        public async Task GetChefIdByUserIdAsyncShouldReturnChefId()
        {
            Guid userId = ChefUser.Id;

            Guid chefId = await this.chefService.GetChefIdByUserIdAsync(userId);

            Assert.That(chefId == Chef.Id);
        }

        [Test]
        public async Task ChefExistsByUserIdAsyncShouldReturnTrueIfExists()
        {
            Guid userId = ChefUser.Id;

            bool result = await this.chefService.ChefExistsByUserIdAsync(userId);

            Assert.That(result);
        }

        [Test]
        public async Task ChefExistsByUserIdAsyncShouldReturnFalseIfDoesNotExists()
        {
            Guid userId = Guid.NewGuid();

            bool result = await this.chefService.ChefExistsByUserIdAsync(userId);

            Assert.That(result == false);
        }

        [Test]
        public async Task ChefExistsByNameAsyncShouldReturnTrueIfExists()
        {
            string name = "Peshoo";

            bool result = await this.chefService.ChefExistsByNameAsync(name);

            Assert.That(result);
        }

        [Test]
        public async Task ChefExistsByNameAsyncShouldReturnFalseIfDoesNotExists()
        {
            string name = "PeshooNE";

            bool result = await this.chefService.ChefExistsByNameAsync(name);

            Assert.That(result == false);
        }

        [Test]
        public async Task ChefExistsByPhoneNumberAsyncShouldReturnTrueIfExists()
        {
            string phone = "0891234567";

            bool result = await this.chefService.ChefExistsByPhoneNumberAsync(phone);

            Assert.That(result);
        }

        [Test]
        public async Task ChefExistsByPhoneNumberAsyncShouldReturnFalseIfDoesNotExists()
        {
            string phone = "0000000000";

            bool result = await this.chefService.ChefExistsByPhoneNumberAsync(phone);

            Assert.That(result == false);
        }
    }
}
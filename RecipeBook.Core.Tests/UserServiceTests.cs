using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Services;
using RecipeBook.Data;

using static RecipeBook.Core.Tests.SeedDatabase;

namespace RecipeBook.Core.Tests
{
    public class UserServiceTests
    {
        private DbContextOptions<RecipeBookDbContext> dbOptions;
        private RecipeBookDbContext dbContext;

        private IUserService userService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RecipeBookDbContext>()
                .UseInMemoryDatabase("RecipeBookInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RecipeBookDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDb(this.dbContext);

            this.userService = new UserService(this.dbContext);
        }

        [Test]
        public async Task GetFullNameAsyncShouldReturnFullName()
        {
            string fullName = $"{User.FirstName} {User.LastName}";

            string result = await this.userService.GetFullNameAsync(User.Email);

            Assert.That(fullName == result);
        }
    }
}

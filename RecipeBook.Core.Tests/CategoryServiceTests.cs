using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Category;
using RecipeBook.Core.Services;
using RecipeBook.Data;

using static RecipeBook.Core.Tests.SeedDatabase;

namespace RecipeBook.Core.Tests
{
    public class CategoryServiceTests
    {
        private DbContextOptions<RecipeBookDbContext> dbOptions;
        private RecipeBookDbContext dbContext;

        private ICategoryService categoryService;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RecipeBookDbContext>()
                .UseInMemoryDatabase("RecipeBookInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RecipeBookDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            await SeedDb(this.dbContext);

            this.categoryService = new CategoryService(this.dbContext);
        }

        [Test]
        public async Task CategoryExistsByIdAsyncShouldReturnTrueIfExists()
        {
            int id = Category1.Id;

            var result = await this.categoryService.CategoryExistsByIdAsync(id);

            Assert.That(result);
        }

        [Test]
        public async Task CategoryExistsByIdAsyncShouldReturnFalseIfDoesNotExists()
        {
            int id = 342;

            var result = await this.categoryService.CategoryExistsByIdAsync(id);

            Assert.That(result == false);
        }

        [Test]
        public async Task CategoryExistsByNameAsyncShouldReturnTrueIfExists()
        {
            string name = Category1.Name;

            var result = await this.categoryService.CategoryExistsByNameAsync(name);

            Assert.That(result);
        }

        [Test]
        public async Task CategoryExistsByNameAsyncShouldReturnFalseIfDoesNotExists()
        {
            string name = "asdasd";

            var result = await this.categoryService.CategoryExistsByNameAsync(name);

            Assert.That(result == false);
        }
    }
}

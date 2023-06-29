using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RecipeBook.Data.Models;

namespace RecipeBook.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            List<Category> categories = new List<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Salad"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Meat"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Seafood"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Soups"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 5,
                Name = "Bread"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 6,
                Name = "Pasta"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 7,
                Name = "Desserts"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}

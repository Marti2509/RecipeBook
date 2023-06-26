using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RecipeBook.Data.Models;

namespace RecipeBook.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //TODO:
            // seed the db with the recipes

            //builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            List<Category> categories = new List<Category>();

            //Category category;

            return categories.ToArray();
        }
    }
}

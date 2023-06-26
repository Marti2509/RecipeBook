using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RecipeBook.Data.Models;

namespace RecipeBook.Data.Configurations
{
    public class RecipeEntityConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder
                .Property(r => r.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);

            builder
                .HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.Chef)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.ChefId)
                .OnDelete(DeleteBehavior.Restrict);

            //TODO:
            // seed the db with the recipes

            //builder.HasData(GenerateRecipes());
        }

        private Recipe[] GenerateRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();

            //Recipe recipe;

            return recipes.ToArray();
        }
    }
}

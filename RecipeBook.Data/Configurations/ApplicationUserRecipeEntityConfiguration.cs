using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RecipeBook.Data.Models;

namespace RecipeBook.Data.Configurations
{
    public class ApplicationUserRecipeEntityConfiguration : IEntityTypeConfiguration<ApplicationUserRecipe>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRecipe> builder)
        {
            builder
                .HasKey(aur => new { aur.RecipeId, aur.UserId });
        }
    }
}

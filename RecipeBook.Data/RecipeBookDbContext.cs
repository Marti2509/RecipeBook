using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using RecipeBook.Data.Configurations;
using RecipeBook.Data.Models;

namespace RecipeBook.Data
{
    public class RecipeBookDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<Chef> Chefs { get; set; } = null!;

        public DbSet<ApplicationUserRecipe> ApplicationUsersRecipes { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(RecipeEntityConfiguration).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
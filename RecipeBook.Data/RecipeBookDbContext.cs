using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeBook.Data
{
    public class RecipeBookDbContext : IdentityDbContext
    {
        public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options)
            : base(options)
        {
        }
    }
}
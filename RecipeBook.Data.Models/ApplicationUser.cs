using Microsoft.AspNetCore.Identity;

namespace RecipeBook.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();

            ApplicationUserRecipes = new List<ApplicationUserRecipe>();
        }

        public ICollection<ApplicationUserRecipe> ApplicationUserRecipes { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static RecipeBook.Common.ModelValidationConstants.User;

namespace RecipeBook.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();

            ApplicationUserRecipes = new List<ApplicationUserRecipe>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public ICollection<ApplicationUserRecipe> ApplicationUserRecipes { get; set; }
    }
}
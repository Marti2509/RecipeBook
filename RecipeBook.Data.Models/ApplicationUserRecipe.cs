using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Data.Models
{
    public class ApplicationUserRecipe
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public int RecipeId { get; set; }

        [Required]
        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static RecipeBook.Common.ModelValidationConstants.Recipe;

namespace RecipeBook.Data.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Comments = new List<Comment>();
            ApplicationUsersRecipe = new List<ApplicationUserRecipe>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public string Products { get; set; } = null!;

        [Required]
        public string Steps { get; set; } = null!;

        [Required]
        public int Servings { get; set; }

        [Required]
        public int CookingTime { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public Guid ChefId { get; set; }

        [Required]
        [ForeignKey(nameof(ChefId))]
        public Chef Chef { get; set; } = null!;

        public ICollection<ApplicationUserRecipe> ApplicationUsersRecipe { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

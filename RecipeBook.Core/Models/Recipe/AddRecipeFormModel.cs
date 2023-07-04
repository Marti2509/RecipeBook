using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static RecipeBook.Common.ModelValidationConstants.Recipe;
using RecipeBook.Core.Models.Category;

namespace RecipeBook.Core.Models.Recipe
{
    public class AddRecipeFormModel
    {
        public AddRecipeFormModel()
        {
            Categories = new List<CategoryViewModel>();
        }

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
        [Range(ServingsMinValue, int.MaxValue, ErrorMessage = "The number can't be 0 or negative")]
        public int Servings { get; set; }

        [Required]
        [Range(CookingTimeMinValue, int.MaxValue, ErrorMessage = "The number can't be 0 or negative")]
        public int CookingTime { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }
}

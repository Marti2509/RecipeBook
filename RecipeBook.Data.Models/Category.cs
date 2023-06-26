using System.ComponentModel.DataAnnotations;

using static RecipeBook.Common.ModelValidationConstants.Category;

namespace RecipeBook.Data.Models
{
    public class Category
    {
        public Category()
        {
            Recipes = new List<Recipe>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        
        public ICollection<Recipe> Recipes { get; set; }
    }
}
using System.ComponentModel;

namespace RecipeBook.Core.Models.Recipe
{
    public class DeleteRecipeViewModel
    {
        [DisplayName("Recipe Name")]
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        [DisplayName("Recipe's category")]
        public string Category { get; set; } = null!;
    }
}

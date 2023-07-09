using RecipeBook.Core.Models.Recipe.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecipeBook.Core.Models.Recipe
{
    public class AllRecipesQueryModel
    {
        public AllRecipesQueryModel()
        {
            Categories = new List<string>();
            Recipes = new List<AllRecipesViewModel>();
        }

        public string? Category { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Recipes By")]
        public RecipeSorting RecipeSorting { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<AllRecipesViewModel> Recipes { get; set; }
    }
}

using RecipeBook.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.Core.Models.Comment;

namespace RecipeBook.Core.Models.Recipe
{
    public class DetailsRecipeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Products { get; set; }

        public string Steps { get; set; }

        public int Servings { get; set; }

        public int CookingTime { get; set; }

        public string Category { get; set; }

        public string Chef { get; set; }

        public Guid ChefId { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}

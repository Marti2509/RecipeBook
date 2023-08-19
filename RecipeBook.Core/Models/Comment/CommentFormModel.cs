using System.ComponentModel.DataAnnotations;

using static RecipeBook.Common.ModelValidationConstants.Comment;

namespace RecipeBook.Core.Models.Comment
{
    public class CommentFormModel
    {
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        public int RecipeId { get; set; }
    }
}

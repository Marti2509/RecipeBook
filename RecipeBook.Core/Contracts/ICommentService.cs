using RecipeBook.Core.Models.Comment;

namespace RecipeBook.Core.Contracts
{
    public interface ICommentService
    {
        public Task AddCommentAsync(CommentFormModel model, Guid userId, int recipeId);
    }
}

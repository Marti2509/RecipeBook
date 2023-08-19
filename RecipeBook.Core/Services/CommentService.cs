using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Comment;
using RecipeBook.Data;
using RecipeBook.Data.Models;

namespace RecipeBook.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly RecipeBookDbContext dbContext;

        public CommentService(RecipeBookDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task AddCommentAsync(CommentFormModel model, Guid userId, int recipeId)
        {
            var comment = new Comment()
            {
                Text = model.Text,
                UserId = userId,
                RecipeId = recipeId
            };

            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<CommentViewModel>> AllCommentsAsync()
        {
            return await dbContext.Comments
                .Where(c => c.IsActive)
                .Select(c => new CommentViewModel()
                {
                    Text = c.Text,
                    User = $"{c.User.FirstName} {c.User.LastName}"
                })
                .ToListAsync();

        }
    }
}

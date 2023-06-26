using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RecipeBook.Data.Models;

namespace RecipeBook.Data.Configurations
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .Property(c => c.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);

            builder
                .HasOne(c => c.Recipe)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            //TODO:
            // seed the db with the recipes

            //builder.HasData(GenerateComments());
        }

        private Comment[] GenerateComments()
        {
            List<Comment> comments = new List<Comment>();

            //Comment comment;

            return comments.ToArray();
        }
    }
}

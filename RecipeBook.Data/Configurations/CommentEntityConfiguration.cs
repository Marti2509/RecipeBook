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
                .Property(r => r.CreatedOn)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

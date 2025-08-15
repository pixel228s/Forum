using Forum.Domain.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Data.Configurations
{
    public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("post_comments", "dbo");

            builder.HasKey(e => e.Id);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(c => c.Text)
                .IsRequired()
                .HasColumnType("nvarchar(3000)");

            builder.HasOne(u => u.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Post)
                .WithMany(c => c.comments)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }

    }
}

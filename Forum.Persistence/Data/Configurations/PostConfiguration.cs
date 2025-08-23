using Forum.Domain.Entities.Posts.Enums;
using Forum.Domain.Models.Posts;
using Forum.Domain.Models.Posts.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Data.Configurations
{
    public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("user_posts", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.State)
                .HasDefaultValue(State.Pending);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.Status).HasDefaultValue(Status.Active);

            builder.Property(x => x.Content)
                .HasColumnType("nvarchar(4000)")
                .IsUnicode(true);

            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(300)")
                .IsUnicode(true);

            builder.HasOne(x => x.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(x => x.State == State.Show && !x.IsDeleted);
        }
    }
}

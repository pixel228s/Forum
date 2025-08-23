using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Data.Configurations
{
    public sealed class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.ToTable("user_bans", "dbo");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.BanReason)
                .HasMaxLength(500)
                .HasColumnType("nvarchar");

            builder.HasOne(u => u.User)
                .WithOne(b => b.BanInfo)
                .HasForeignKey<Ban>(b => b.UserId);
        }
    }
}

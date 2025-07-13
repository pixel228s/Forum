using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Data.Configurations
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.ToTable("user_bans", "dbo");

            builder.HasKey(x => x.Id);

            builder.HasOne(u => u.User)
                .WithOne(b => b.BanInfo)
                .HasForeignKey<Ban>(b => b.UserId);
        }
    }
}

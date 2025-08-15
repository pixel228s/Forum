using Forum.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Data.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", "dbo");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.RefreshToken).HasMaxLength(50);

            builder.Property(x => x.IsAdmin).HasDefaultValue(false);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(120)
                .IsFixedLength()
                .HasColumnType("varchar")
                .IsUnicode(false);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.IsBanned).HasDefaultValue(false);

            builder
                .Ignore(user => user.ConcurrencyStamp)
                .Ignore(user => user.PhoneNumber)
                .Ignore(user => user.PhoneNumberConfirmed)
                .Ignore(user => user.TwoFactorEnabled)
                .Ignore(user => user.LockoutEnabled)
                .Ignore(user => user.LockoutEnd)
                .Ignore(user => user.AccessFailedCount);
        }
    }
}

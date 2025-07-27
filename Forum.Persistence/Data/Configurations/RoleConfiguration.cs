using Forum.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Data.Configurations
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Ignore(x => x.ConcurrencyStamp);

            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Admin role for the user"
                },
                new Role
                {
                    Id = 2,
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    Description = "Member role for the user"
                }
            );   
        }
    }
}

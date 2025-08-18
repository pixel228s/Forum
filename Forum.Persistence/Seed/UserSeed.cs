using Forum.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Persistence.Seed
{
    public static class UserSeed
    {
        public static async Task SeedAdmin(this IServiceProvider serviceProvider)
        {
            using var service = serviceProvider.CreateScope();
            var userManagerService = service.ServiceProvider.GetRequiredService<UserManager<User>>();
            string email = "Admin123@yopmail.com";

            var user = await userManagerService.FindByEmailAsync(email);
            if (user == null)
            {
                var password = "Admin@123!";
                var admin = new User
                {
                    FirstName = "Salome",
                    LastName = "Admin",
                    Email = email,
                    UserName = "admin",
                    IsAdmin = true,
                };

                await userManagerService.CreateAsync(admin, password).ConfigureAwait(false);
                await userManagerService.AddToRoleAsync(admin, "Admin").ConfigureAwait(false);
            }
        }
    }
}

using Forum.Domain.Interfaces;
using Forum.Persistence.Data;
using Forum.Persistence.HealthCheck;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ForumDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ForumDbContext).Assembly.FullName)));
            return services;
        }

        public static IServiceCollection AddHealthCheckService(this IServiceCollection services)
        {
            services.AddScoped<IHealthCheckService, HealthCheckService>();
            return services;
        }
    }
}

using Forum.Domain.Interfaces;
using Forum.Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection addServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Forum.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection addMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Ref).Assembly));
            return services;
        }

        public static IServiceCollection addAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Ref).Assembly);
            return services;
        }
    }
}

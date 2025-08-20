using FluentValidation;
using Forum.Application.Common.Behaviors;
using Forum.Application.Common.EmailSender.Models;
using Forum.Application.Common.EmailSender;
using Forum.Application.Common.SecurityService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection addMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
            { 
                cfg.RegisterServicesFromAssembly(typeof(Ref).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehaviors<,>));
            });
            return services;
        }

        public static IServiceCollection addAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Ref).Assembly);
            return services;
        }

        public static IServiceCollection AddCustomValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Ref).Assembly, includeInternalTypes: true);
            return services;
        }

        public static IServiceCollection AddTokenProvider(this IServiceCollection services)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();
            return services;
        }

        public static IServiceCollection AddMailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.Configure<EmailSenderOptions>(configuration.GetSection(EmailSenderOptions.Key));

            return services;
        }

        public static IServiceCollection AddCachingService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = configuration.GetConnectionString("Redis")!;
                redisOptions.Configuration = connection;
            });
            return services;
        }
    }
}

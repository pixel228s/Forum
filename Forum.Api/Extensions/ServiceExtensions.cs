using Asp.Versioning;
using Asp.Versioning.Conventions;
using Forum.Application.Common.CustomTokenProviders;
using Forum.Domain.Models.Users;
using Forum.Persistence.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
using System.Text;

namespace Forum.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
             services.AddIdentity<User, Role>(options =>
             {
                 options.User.RequireUniqueEmail = true;
                 options.Password = new PasswordOptions
                 {
                     RequireDigit = true,
                     RequireLowercase = true,
                     RequireNonAlphanumeric = true,
                     RequireUppercase = true,
                     RequiredLength = 8,
                     RequiredUniqueChars = 3,
                 };
             })
            .AddEntityFrameworkStores<ForumDbContext>()
            .AddTokenProvider<ResetPasswordTokenProvider<User>>("ResetPassword")
            .AddDefaultTokenProviders();
            services
                .Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(1));
            return services;
        }

        public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Description = "Enter the token with the `Bearer: ` prefix, e.g. \"Bearer abcde12345\". without the double quotes"
                    });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement { 
                        {
                            new OpenApiSecurityScheme {
                                Reference = 
                                new OpenApiReference 
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer" 
                                } 
                            },
                            new string[] { } 
                        } 
                    });
            });

            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretForKey"]))
                };
            });
            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opts =>
            {
                opts.ApiVersionReader = new UrlSegmentApiVersionReader();
                opts.DefaultApiVersion = new(1, 0);
                opts.AssumeDefaultVersionWhenUnspecified = true;
            })
            .AddMvc()
            .AddApiExplorer(opts =>
            {
                opts.GroupNameFormat = "'v'V";
                opts.SubstituteApiVersionInUrl = true;
            });
            return services;
        }
    }
}

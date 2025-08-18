using Amazon.S3;
using Forum.Domain.Interfaces;
using Forum.Infrastructure.Implementations;
using Forum.Infrastructure.Services.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection addServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IBanRepository, BanRepository>();

            services.AddScoped<ITransactionFactory, TransactionFactory>();
            services.AddScoped<IS3Service, S3Service>();
            services.AddSingleton<IAmazonS3>(sp =>
            {
                return new AmazonS3Client(
                    configuration["AWS:AccessKey"],
                    configuration["AWS:SecretKey"],
                    Amazon.RegionEndpoint.APSouth1
                );
            });
            return services;
        }
    }
}

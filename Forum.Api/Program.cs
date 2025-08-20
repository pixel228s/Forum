using Forum.Api.Extensions;
using Forum.Api.Infrastructure.StartupConfigurations;
using Forum.Application.DependencyInjection;
using Forum.Infrastructure.DependencyInjection;
using Forum.Persistence;
using Forum.Persistence.Seed;
namespace Forum.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddVersioning()
                .AddCachingService(builder.Configuration)
                .AddSwaggerConfigurations()
                .AddHealthCheckService()
                .AddIdentity()
                .AddMailSender(builder.Configuration)
                .AddJwtConfiguration(builder.Configuration)
                .AddPersistence(builder.Configuration)
                .AddTokenProvider()
                .AddCustomValidation()
                .addServices(builder.Configuration)
                .addAutoMapper()
                .addMediator();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<BanCheckMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
           
            app.MapControllers();

            await app.Services.SeedAdmin();

            app.Run();
        }
    }
}

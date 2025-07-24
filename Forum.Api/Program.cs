using Forum.Api.Infrastructure.StartupConfigurations;
using Forum.Application.DependencyInjection;
using Forum.Domain.Models.Users;
using Forum.Infrastructure.DependencyInjection;
using Forum.Persistence;
using Forum.Persistence.Data;
using Microsoft.AspNetCore.Identity;

namespace Forum.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ForumDbContext>();


            builder.Services
                .AddPersistence(builder.Configuration)
                .AddCustomValidation()
                .addServices()
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
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

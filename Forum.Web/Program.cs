using Forum.Application.DependencyInjection;
using Forum.Application;
using Forum.Infrastructure.DependencyInjection;
using Forum.Persistence;
using Forum.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Forum.Persistence.Data;
using Forum.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Forum.Web.Helper.Middlewares;

namespace Forum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Ref).Assembly)
            );


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services
                  .AddEndpointsApiExplorer()
                  .AddVersioning()
                  .AddCachingService(builder.Configuration)
                  .AddHealthCheckService()
                  .AddMailSender(builder.Configuration)
                  .AddIdentity()
                  .AddJwtConfiguration(builder.Configuration)
                  .AddPersistence(builder.Configuration)
                  .AddTokenProvider()
                  .AddCustomValidation()
                  .addServices(builder.Configuration)
                  .addAutoMapper()
                  .addMediator();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseJwtMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

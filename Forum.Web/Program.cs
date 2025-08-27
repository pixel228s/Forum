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
using Forum.Api.Infrastructure.StartupConfigurations;

namespace Forum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
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



            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Error");
            //    app.UseHsts();
            //}
            //else
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseMiddleware<WebExceptionMiddleware>();

            app.UseJwtMiddleware();
            app.UseAuthentication();

            app.UseMiddleware<CheckBanWebMiddleware>();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

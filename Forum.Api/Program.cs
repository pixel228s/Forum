using Forum.Application.Features.UserFeatures.Queries;
using Forum.Application.messaging.Commands;
using Forum.Application.messaging.Queries;
using Forum.Persistence;
using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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

            builder.Services.AddPersistence(builder.Configuration);

            var assembly = Assembly.GetAssembly(typeof(GetUserByIdQuery));

            var list = assembly.GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract)
                .SelectMany(x => x.GetInterfaces(), (type, iface) => new { type, iface })
                .Where(x => x.iface.IsGenericType && (x.iface.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) 
                || x.iface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .ToList();

            foreach (var iface in list)
            {
                builder.Services.AddScoped(iface.iface, iface.type);
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

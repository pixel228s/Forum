using Forum.Application.DependencyInjection;
using Forum.Infrastructure.DependencyInjection;
using Forum.Persistence;

namespace Forum.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddPersistence(builder.Configuration)
                      .addServices(builder.Configuration)
                      .AddBanService()
                      .AddHostedService<RevokeExpiredBansWorker>()
                      .AddHostedService<InactivateExpiredPosts>();

            var host = builder.Build();
            host.Run();
        }
    }
}
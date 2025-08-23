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
                      .AddHostedService<RevokeExpiredBansWorker>();

            var host = builder.Build();
            host.Run();
        }
    }
}
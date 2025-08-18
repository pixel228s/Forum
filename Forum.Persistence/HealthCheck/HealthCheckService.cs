using Forum.Domain.Interfaces;
using Forum.Persistence.Data;

namespace Forum.Persistence.HealthCheck
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly ForumDbContext _context;

        public HealthCheckService(ForumDbContext context)
        {
            _context = context;
        }

        public async Task<string> CheckDatabaseHealth(CancellationToken cancellationToken = default)
        {
            bool result = await _context.Database
                .CanConnectAsync(cancellationToken)
                .ConfigureAwait(false);
            return result ? "Healthy" : "Unhealthy";
        }
    }
}

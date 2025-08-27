using Forum.Application.Common.RevokeExpiredBans;
using Forum.Domain.Interfaces;

namespace Forum.Worker
{
    public class RevokeExpiredBansWorker : IHostedService, IDisposable
    {
        private readonly ILogger<RevokeExpiredBansWorker> _logger;
        private readonly CancellationTokenSource _tokenSource;
        private readonly IServiceProvider _serviceProvider;
        private Task? task;

        public RevokeExpiredBansWorker(ILogger<RevokeExpiredBansWorker> logger, 
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _tokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            GC.SuppressFinalize(this);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            task = ExecuteAsync(_tokenSource.Token);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (task != null)
            {
                _tokenSource?.Cancel();
                await Task.WhenAny(task, Task.Delay(Timeout.Infinite, cancellationToken));   
            }
        }

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {             
                try
                {
                    using var service = _serviceProvider.CreateScope();
                    var requiredService = service.ServiceProvider
                        .GetRequiredService<IBanService>();

                    await requiredService.RevokeExpiredBans(stoppingToken).ConfigureAwait(false);
                }
                catch(Exception ex) 
                {
                    _logger.LogError($"Worker threw an error while removing a ban: {ex.Message}");
                }
                var nextRun = DateTime.Today.AddDays(1).AddHours(2) - DateTime.Now;
                await Task.Delay(nextRun, stoppingToken);
            }
        }
    }
}

namespace Forum.Domain.Interfaces
{
    public interface IHealthCheckService
    {
        Task<string> CheckDatabaseHealth(CancellationToken cancellationToken);    
    }
}

using Forum.Domain.Models;

namespace Forum.Domain.Interfaces
{
    public interface IBanRepository : IBaseRepository<Ban>
    {
        Task<Ban?> GetBanById (int id, CancellationToken cancellationToken);
        Task<IEnumerable<Ban>> GetAllBans(CancellationToken cancellationToken);
        Task<IEnumerable<Ban>> GetExpiredBans(CancellationToken cancellationToken);
    }
}

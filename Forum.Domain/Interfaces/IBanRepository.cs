using Forum.Domain.Models;
using Forum.Domain.Parameters;

namespace Forum.Domain.Interfaces
{
    public interface IBanRepository : IBaseRepository<Ban>
    {
        Task<Ban?> GetBanById (int id, CancellationToken cancellationToken);
        Task<IEnumerable<Ban>> GetAllBans(RequestParameters requestParameters, CancellationToken cancellationToken);
        Task<IEnumerable<int>> GetExpiredBans(CancellationToken cancellationToken);
        Task<int> DeleteExpiredBans(CancellationToken cancellationToken);
    }
}

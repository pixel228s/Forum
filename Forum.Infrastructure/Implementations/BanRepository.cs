using Forum.Domain.Interfaces;
using Forum.Domain.Models;
using Forum.Persistence.Data;

namespace Forum.Infrastructure.Implementations
{
    public class BanRepository : BaseRepository<Ban>, IBanRepository
    {
        public BanRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<Ban>> GetAllBans(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Ban> GetBanById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ban>> GetExpiredBans(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

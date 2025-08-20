using Forum.Domain.Interfaces;
using Forum.Domain.Models;
using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Implementations
{
    public class BanRepository : BaseRepository<Ban>, IBanRepository
    {
        public BanRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Ban>> GetAllBans(CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<Ban?> GetBanById(int id, CancellationToken cancellationToken)
        {
            return _dbSet.AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ban>> GetExpiredBans(CancellationToken cancellationToken)
        {
            var expiredBans = await _dbSet
                .AsNoTracking()
                .Where(x => x.BanEndDate > DateTime.UtcNow)
                .ToListAsync(cancellationToken);
            return expiredBans;
        }
    }
}

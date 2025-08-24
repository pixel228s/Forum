using Forum.Domain.Interfaces;
using Forum.Domain.Models;
using Forum.Domain.Parameters;
using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Implementations
{
    public class BanRepository : BaseRepository<Ban>, IBanRepository
    {
        public BanRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Ban>> GetAllBans(RequestParameters requestParameters, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                .Take(requestParameters.PageSize)
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

        public async Task<int> DeleteExpiredBans(CancellationToken cancellationToken)
        {
            var deletedColumns = await 
                _dbSet.Where(x => x.BanEndDate <= DateTime.UtcNow)
                .ExecuteDeleteAsync(cancellationToken);
            return deletedColumns;
        }
    }
}

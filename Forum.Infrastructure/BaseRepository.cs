using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure
{
    public class BaseRepository<T> where T : class
    {
        protected readonly ForumDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<List<T>?> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetAsync(object param, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(param, cancellationToken);
        }

        public async Task CreateAsync(CancellationToken cancellationToken, T entity)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, T entity)
        {
            _dbSet.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}


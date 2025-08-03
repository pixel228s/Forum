using Forum.Domain.Interfaces;
using Forum.Domain.Models.Base;
using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ForumDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T?> GetAsync(CancellationToken cancellationToken, params object[] key)
        {
            return await _dbSet.FindAsync(key, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeactivateEntity(T Entity, CancellationToken cancellationToken)
        {
            Entity.IsDeleted = true;

            _dbContext.Update(Entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                return;
            }

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateEntity(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}


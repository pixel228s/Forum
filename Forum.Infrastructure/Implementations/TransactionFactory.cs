using Forum.Domain.Interfaces;
using Forum.Domain.Models.Base;
using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Forum.Infrastructure.Implementations
{
    public class TransactionFactory : ITransactionFactory
    {
        private readonly ForumDbContext _context;
        public TransactionFactory(ForumDbContext context) 
        {
            _context = context;
        }

        public async Task<DbTransaction> OpenTransactionAsync(CancellationToken cancellationToken)
        {
            var transaction = await _context.Database
                .BeginTransactionAsync(cancellationToken)
                .ConfigureAwait(false);
            return transaction.GetDbTransaction();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            EntityPreparations();
            await _context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        private void EntityPreparations()
        {
            var entities = _context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(c => c.State == EntityState.Modified || c.State == EntityState.Added);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTime.UtcNow;
                }

                entity.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}

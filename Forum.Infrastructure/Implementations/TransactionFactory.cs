using Forum.Domain.Interfaces;
using Forum.Persistence.Data;
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
    }
}

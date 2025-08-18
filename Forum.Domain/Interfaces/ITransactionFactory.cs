using System.Data.Common;

namespace Forum.Domain.Interfaces
{
    public interface ITransactionFactory
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<DbTransaction> OpenTransactionAsync(CancellationToken cancellationToken);
    }
}

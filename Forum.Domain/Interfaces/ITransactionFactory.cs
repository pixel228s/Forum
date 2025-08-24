using System.Data.Common;

namespace Forum.Domain.Interfaces
{
    public interface ITransactionFactory
    {
        Task<DbTransaction> OpenTransactionAsync(CancellationToken cancellationToken);
    }
}

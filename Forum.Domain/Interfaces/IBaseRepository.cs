using System.Threading;

namespace Forum.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetAsync(CancellationToken cancellationToken, params object[] key);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task RemoveAsync(T entity, CancellationToken cancellationToken);
        Task UpdateEntity(T entity, CancellationToken cancellationToken);
    }
}

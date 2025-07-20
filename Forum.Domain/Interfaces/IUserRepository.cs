using Forum.Domain.Models.Users;

namespace Forum.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id, CancellationToken cancellationToken);
        Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    }
}

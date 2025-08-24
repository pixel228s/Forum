using Forum.Domain.Models.Users;
using Forum.Domain.Parameters;

namespace Forum.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id, CancellationToken cancellationToken);
        Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAllUsers(RequestParameters requestParameters, CancellationToken cancellationToken);
    }
}

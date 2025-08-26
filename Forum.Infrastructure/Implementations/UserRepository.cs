using Forum.Domain.Entities.Posts.Enums;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Users;
using Forum.Domain.Parameters;
using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumDbContext _forumDbContext;
        public UserRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            var user = _forumDbContext.Users
               .Where(u => u.Email == email)
               .AsNoTracking()
               .FirstOrDefaultAsync(cancellationToken);
            return user; 
        }

        public Task<User?> GetUserById(int id, CancellationToken cancellationToken)
        {
            var user = _forumDbContext.Users
               .AsNoTracking()
               .Where(u => u.Id == id)
               .FirstOrDefaultAsync(cancellationToken);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers(RequestParameters requestParameters, CancellationToken cancellationToken)
        {
            var users = await _forumDbContext.Users
                .AsNoTracking()
                .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                .Take(requestParameters.PageSize)
                .ToListAsync(cancellationToken);
            return users;
        }

        public async Task<int> UpdateBannedUsers(CancellationToken cancellationToken)
        {
            var updatedColumns = await _forumDbContext.Users
                .Where(u => u.BanInfo != null && u.BanInfo.BanEndDate <= DateTime.UtcNow)
                .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.IsBanned, false),
                    cancellationToken);
            return updatedColumns;
        }
    }
}

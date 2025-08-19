using Forum.Domain.Interfaces;
using Forum.Domain.Models.Users;
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

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _forumDbContext.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return users;
        }
    }
}

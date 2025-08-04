using Forum.Domain.Entities.Comments;
using Forum.Domain.Interfaces;
using Forum.Infrastructure.Extensions;
using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Implementations
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IQueryable<CommentWithUserInfo>> GetCommentsByPostId(int postID, bool include, CancellationToken cancellationToken)
        {
            var comments = _dbSet
                .AsNoTracking()
                .Where(x => x.PostId == postID)
                .CustomInclude(u => u.User, include)
                .OrderByDescending(x => x.CreatedAt)
                .Select(comment => new CommentWithUserInfo
                {
                    Comment = comment,
                    UserName = comment.User.UserName!,
                    UserProfilePicUrl = comment.User.picUrl,
                });

            return Task.FromResult(comments);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserId(int userId, CancellationToken cancellationToken)
        {
            var comments = await _dbSet
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);

            return comments;
        }
    }
}

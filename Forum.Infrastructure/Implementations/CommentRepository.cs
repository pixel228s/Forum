using Forum.Domain.Entities.Comments;
using Forum.Domain.Interfaces;
using Forum.Domain.Parameters;
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

        public async Task DeleteUserComments(int userId, CancellationToken cancellationToken)
        {
            await _dbSet.Where(x => x.UserId == userId)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public Task<Comment?> GetCommentById(int id, bool isIncluded, bool postIncluded, CancellationToken cancellationToken)
        {
            var comment = _dbSet
                .AsNoTracking()
                .Where(x => x.Id == id)
                .CustomInclude(c => c.User, isIncluded)
                .CustomInclude(p => p.Post, postIncluded)
                .FirstOrDefaultAsync();
            return comment;
        }

        public Task<IQueryable<CommentWithUserInfo>> GetCommentsByPostId(int postID, bool include, RequestParameters requestParameters, CancellationToken cancellationToken)
        {
            var comments = _dbSet
                .AsNoTracking()
                .Where(x => x.PostId == postID)
                .CustomInclude(u => u.User, include)
                .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                .Take(requestParameters.PageSize)
                .OrderByDescending(x => x.CreatedAt)
                .Select(comment => new CommentWithUserInfo
                {
                    Comment = comment,
                    UserName = comment.User.UserName!,
                    UserProfilePicUrl = comment.User.picUrl,
                })
                ;

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

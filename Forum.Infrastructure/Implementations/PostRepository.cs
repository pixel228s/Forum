using Forum.Domain.Entities.Posts;
using Forum.Domain.Entities.Posts.Enums;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Posts;
using Forum.Domain.Models.Posts.Enums;
using Forum.Domain.Parameters;
using Forum.Infrastructure.Extensions;
using Forum.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Implementations
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeactivatePosts(CancellationToken cancellationToken)
        {
            DateTime difference = DateTime.Now.AddDays(-7);
            await _dbSet
                  .Where(p => p.Status == Status.Active)
                  .Where(p => !p.comments!.Any() && p.CreatedAt < difference)
                  .ExecuteUpdateAsync(
                      setters => setters.SetProperty(p => p.Status, Status.Inactive),
                      cancellationToken);
        }

        public async Task<IEnumerable<PostWithCommentCount>> GetAllPosts(RequestParameters requestParameters, CancellationToken cancellationToken)
        {
            var posts = await _dbSet
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                .Take(requestParameters.PageSize)
                .Select(post => new PostWithCommentCount
                {
                    post = post,
                    authorUsername = post.User.UserName!,
                    authorPfp = post.User.picUrl,
                    commentCount = post.comments!.Count()
                })
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return posts;
        }

        public async Task<IEnumerable<Post>> GetPendingPosts(RequestParameters requestParameters, CancellationToken cancellationToken)
        {
            var hiddenPosts = await 
                _dbSet
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(x => x.State == State.Pending)
                .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                .Take(requestParameters.PageSize)
                .ToListAsync(cancellationToken);

            return hiddenPosts;
        }

        public Task<Post?> GetPostByIdAsync(int id, CancellationToken cancellationToken, bool isIncluded, bool isAllowed)
        {
            var post = _dbSet
                .AsNoTracking()
                .AllowQueryFilters(isAllowed)
                .Where(x => x.Id == id)
                .CustomInclude(x => x.User!, isIncluded)
                .FirstOrDefaultAsync(cancellationToken);

            return post;
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(int userId, CancellationToken cancellationToken)
        {
            var posts = await _dbSet
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return posts;
        }
    }
}

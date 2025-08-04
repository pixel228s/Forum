using Forum.Domain.Entities.Posts;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Posts;
using Forum.Domain.Models.Posts.Enums;
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

        public async Task<IEnumerable<PostWithCommentCount>> GetAllPosts(CancellationToken cancellationToken)
        {
            var posts = await _dbSet
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(post => new PostWithCommentCount
                {
                    post = post,
                    authorUsername = post.User.UserName!,
                    authorPfp = post.ImageUrl,
                    commentCount = post.comments!.Count()
                })
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return posts;
        }

        public async Task<IEnumerable<Post>> GetHiddenTopics(CancellationToken cancellationToken)
        {
            var hiddenPosts = await 
                _dbSet
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(x => x.State == State.Hide)
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
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);

            return posts;
        }
    }
}

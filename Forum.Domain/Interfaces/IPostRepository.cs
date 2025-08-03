using Forum.Domain.Entities.Posts;
using Forum.Domain.Models.Posts;

namespace Forum.Domain.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<PostWithCommentCount>> GetAllPosts(CancellationToken cancellationToken);
        Task<Post?> GetPostByIdAsync(int id, CancellationToken cancellationToken, bool isIncluded);
        Task<IEnumerable<Post>> GetPostsWithCommentsByUser(string username, CancellationToken cancellationToken);
        Task<int> GetPostCountByUserAsync(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<Post>> GetHiddenTopics(CancellationToken cancellationToken);
    }
}

using Forum.Domain.Entities.Posts;
using Forum.Domain.Models.Posts;

namespace Forum.Domain.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<PostWithCommentCount>> GetAllPosts(CancellationToken cancellationToken);
        Task<Post?> GetPostByIdAsync(int id, CancellationToken cancellationToken, bool isIncluded, bool isAllowed);
        Task<IEnumerable<Post>> GetPostsByUserId(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<Post>> GetHiddenTopics(CancellationToken cancellationToken);
    }
}

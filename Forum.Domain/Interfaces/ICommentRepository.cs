using Forum.Domain.Entities.Comments;
using Forum.Domain.Parameters;

namespace Forum.Domain.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IEnumerable<CommentWithUserInfo>> GetCommentsByPostId(int postID, bool include, CancellationToken cancellationToken);
        Task<IEnumerable<Comment>> GetCommentsByUserId(int userId, CancellationToken cancellationToken); 
        Task<Comment?> GetCommentById (int id, bool isIncluded, bool postIncluded, CancellationToken cancellationToken);
        Task DeleteUserComments(int userId, CancellationToken cancellationToken);
    }
}

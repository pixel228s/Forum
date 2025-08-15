using Forum.Domain.Entities.Comments;

namespace Forum.Domain.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IQueryable<CommentWithUserInfo>> GetCommentsByPostId(int postID, bool include, CancellationToken cancellationToken);
        Task<IEnumerable<Comment>> GetCommentsByUserId(int userId, CancellationToken cancellationToken); 
    }
}

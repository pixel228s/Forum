using Forum.Domain.Entities.Comments;

namespace Forum.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IQueryable<CommentWithUserInfo>> GetCommentsByPostId(int postID, bool include, CancellationToken cancellationToken);
    }
}

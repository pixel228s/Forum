using MediatR;

namespace Forum.Application.Features.CommentFeatures.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<Unit>
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
    }
}

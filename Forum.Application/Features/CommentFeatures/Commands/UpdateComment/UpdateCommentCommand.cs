using Forum.Application.Common.Dtos.Comments.Responses;
using MediatR;

namespace Forum.Application.Features.CommentFeatures.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<CommentResponseDto>
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}

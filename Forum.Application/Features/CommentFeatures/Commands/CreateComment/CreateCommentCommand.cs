using Forum.Application.Common.Dtos.Comments.Responses;
using MediatR;

namespace Forum.Application.Features.CommentFeatures.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<CommentResponseDto>
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}

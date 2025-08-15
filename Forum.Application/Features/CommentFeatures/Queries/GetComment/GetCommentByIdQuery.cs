using Forum.Application.Common.Dtos.Comments.Responses;
using MediatR;

namespace Forum.Application.Features.CommentFeatures.Queries.GetComment
{
    public class GetCommentByIdQuery : IRequest<CommentResponseDto>
    {
        public int Id { get; set; }
    }
}

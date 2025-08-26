using Forum.Application.Common.Dtos.Comments.Responses;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.RetrievePostComments
{
    public class GetPostCommentsByIdQuery : IRequest<IEnumerable<CommentResponseDto>>
    {
        public int PostId { get; set; }
    }
}

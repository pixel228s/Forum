using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Domain.Parameters;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.RetrievePostComments
{
    public class GetPostCommentsByIdQuery : IRequest<IEnumerable<CommentResponseDto>>
    {
        public int PostId { get; set; }
        public RequestParameters parameters { get; set; }
    }
}

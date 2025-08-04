using Forum.Application.Common.Dtos.Comments.Responses;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.GetUserComments
{
    public class GetUserCommentsQuery : IRequest<IEnumerable<CommentResponseDto>>
    {
        public int Id { get; set; }
    }
}

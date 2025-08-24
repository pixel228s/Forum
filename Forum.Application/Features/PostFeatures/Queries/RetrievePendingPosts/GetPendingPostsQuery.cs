using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Domain.Parameters;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.RetrievePendingPosts
{
    public record GetPendingPostsQuery(RequestParameters parameters) : IRequest<IEnumerable<PostResponse>>;
}

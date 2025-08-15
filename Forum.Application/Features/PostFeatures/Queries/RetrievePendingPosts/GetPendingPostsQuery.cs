using Forum.Application.Common.Dtos.Posts.Responses;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.RetrievePendingPosts
{
    public record GetPendingPostsQuery() : IRequest<IEnumerable<PostResponse>>;
}

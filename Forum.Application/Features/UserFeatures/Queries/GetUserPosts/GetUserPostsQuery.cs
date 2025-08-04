using Forum.Application.Common.Dtos.Posts.Responses;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.GetUserPosts
{
    public record GetUserPostsQuery(int Id) : IRequest<IEnumerable<PostResponse>>;
}

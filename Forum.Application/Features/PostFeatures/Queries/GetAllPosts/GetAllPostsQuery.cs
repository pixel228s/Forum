using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Domain.Entities.Posts;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.GetAllPosts
{
    public record GetAllPostsQuery() : IRequest<IEnumerable<PostResponse>>;
}

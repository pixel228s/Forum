using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Domain.Parameters;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.GetAllPosts
{
    public record GetAllPostsQuery(RequestParameters parameters) : IRequest<IEnumerable<PostResponse>>;
}

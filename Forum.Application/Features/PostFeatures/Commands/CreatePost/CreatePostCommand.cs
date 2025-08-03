using Forum.Application.Common.Dtos.Posts.Responses;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Commands.CreatePost
{
    public record CreatePostCommand(
        string post, 
        string? picUrl, 
        string? Title) : IRequest<PostResponse>
    {
    }
}

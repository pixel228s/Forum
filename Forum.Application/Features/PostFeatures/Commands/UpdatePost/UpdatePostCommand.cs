using Forum.Application.Common.Dtos.Posts.Responses;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest<PostResponse>
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}

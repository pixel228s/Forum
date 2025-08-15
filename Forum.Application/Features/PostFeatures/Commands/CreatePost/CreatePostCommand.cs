using Forum.Application.Common.Dtos.Posts.Responses;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<PostResponse>
    {
        public required string userId {  get; set; }
        public required string Content { get; set; }
        public string? PicUrl {  get; set; }
        public string? Title { get; set; }
    }
}

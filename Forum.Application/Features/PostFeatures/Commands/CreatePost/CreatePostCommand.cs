using Forum.Application.Common.Dtos.Posts.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Forum.Application.Features.PostFeatures.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<PostResponse>
    {
        public required int userId {  get; set; }
        public required string Content { get; set; }
        public string? Title { get; set; }
        public IFormFile? Image { get; set; }
    }
}

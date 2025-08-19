using Microsoft.AspNetCore.Http;

namespace Forum.Application.Common.Dtos.Posts.Requests
{
    public class CreatePostRequest
    {
        public required string Content { get; set; }
        public string? Title { get; set; }
        public IFormFile? Image { get; set; }
    }
}

using Forum.Domain.Models.Posts.Enums;

namespace Forum.Application.Common.Dtos.Posts.Responses
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public string AuthorUsername { get; set; }
        public string? UserProfilePicUrl { get; set; }
        public State State { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Forum.Application.Common.Dtos.Comments.Responses
{
    public class CommentResponseDto
    {
        [JsonPropertyName("comment-id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("user_pfp")]
        public string? UserProfilePicUrl { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("post-id")]
        public int PostId { get; set; }
        [JsonPropertyName("creation-date")]
        public DateTime CreationDate { get; set; }
    }
}

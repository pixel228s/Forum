using System.Text.Json.Serialization;

namespace Forum.Application.Features.UserFeatures.Queries.Models
{
    public class UserResponse
    {
        [JsonPropertyName("user_id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}

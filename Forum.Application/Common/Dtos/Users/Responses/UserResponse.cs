using System.Text.Json.Serialization;

namespace Forum.Application.Common.Dtos.Users.Responses
{
    public class UserResponse
    {
        [JsonPropertyName("user_id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}

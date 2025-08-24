using System.Text.Json.Serialization;

namespace Forum.Application.Common.Dtos.Users.Responses
{
    public class UserResponse
    {
        [JsonPropertyName("user_id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("pic_url")]
        public string? picUrl { get; set; }
    }
}

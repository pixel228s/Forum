using System.Text.Json.Serialization;

namespace Forum.Application.Common.Dtos.Auth.Responses
{
    public class TokenDto
    {
        [JsonPropertyName("token")]
        public required string Token { get; set; }
        [JsonPropertyName("refresh-token")]
        public required string RefreshToken { get; set; }
    }
}

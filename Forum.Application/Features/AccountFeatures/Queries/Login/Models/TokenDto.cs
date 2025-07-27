using System.Text.Json.Serialization;

namespace Forum.Application.Features.AccountFeatures.Queries.Login.Models
{
    public class TokenDto
    {
        [JsonPropertyName("token")]
        public required string Token { get; set; }
        [JsonPropertyName("refresh-token")]
        public required string RefreshToken { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Forum.Application.Features.AccountFeatures.Queries.Login.Models
{
    public class TokenDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("refresh-token")]
        public string RefreshToken { get; set; }
        //[JsonPropertyName("username")]
        //public string Username { get; set; }
        //[JsonPropertyName("email")]
        //public string Email { get; set; }
        //[JsonPropertyName("first_name")]
        //public string FirstName { get; set; }
        //[JsonPropertyName("last_name")]
        //public string LastName { get; set; }
    }
}

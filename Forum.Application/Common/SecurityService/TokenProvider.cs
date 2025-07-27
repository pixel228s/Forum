using Forum.Domain.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Forum.Application.Common.SecurityService
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(User user, IList<string> roles)
        {
            var authConfigs = _configuration.GetSection("Authentication");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfigs["SecretForKey"]));
            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
               new(ClaimTypes.Name, user.UserName),
               new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: authConfigs["Issuer"],
                audience: authConfigs["Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(10),
                signInCredentials
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return tokenToReturn;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}

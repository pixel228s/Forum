using Forum.Application.Features.AccountFeatures.Queries.Login.Models;
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

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<TokenDto> CreateToken(User user, IList<string> roles)
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
            var refreshToken = GenerateRefreshToken();

            return new TokenDto 
            { 
                RefreshToken = refreshToken,
                Token = tokenToReturn
            };
        }

        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var jwtSetting = _configuration.GetSection("Authentication");
            var tokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = _configuration["Issuer"],
                ValidAudience = _configuration["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretForKey"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken != null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("invalid token");
            }

            return principal;   
        }
    }
}

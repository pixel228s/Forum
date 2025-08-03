using Forum.Application.Common.Dtos.Auth.Responses;
using Forum.Domain.Models.Users;
using System.Security.Claims;

namespace Forum.Application.Common.SecurityService
{
    public interface ITokenProvider
    {
        Task<TokenDto> CreateToken(User user, IList<string> roles);
        ClaimsPrincipal GetClaimsPrincipal(string token);
    }
}

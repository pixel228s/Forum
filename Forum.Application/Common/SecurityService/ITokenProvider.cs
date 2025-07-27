using Forum.Application.Features.AccountFeatures.Queries.Login.Models;
using Forum.Domain.Models.Users;
using System.Security.Claims;

namespace Forum.Application.Common.SecurityService
{
    public interface ITokenProvider
    {
        Task<TokenDto> CreateToken(User user, IList<string> roles, bool populateDate);
        ClaimsPrincipal GetClaimsPrincipal(string token);

        //string GenerateRefreshToken();
    }
}

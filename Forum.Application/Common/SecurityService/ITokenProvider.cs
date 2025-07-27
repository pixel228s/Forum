using Forum.Domain.Models.Users;

namespace Forum.Application.Common.SecurityService
{
    public interface ITokenProvider
    {
        string GetToken(User user, IList<string> roles);
        string GenerateRefreshToken();
    }
}

using System.Security.Claims;

namespace Forum.Application.Common.Behaviors
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(value, out var id) ? id : throw new UnauthorizedAccessException("Invalid user Id.");
        }
    }
}

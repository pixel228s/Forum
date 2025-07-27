using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Common.CustomTokenProviders
{
    public class ResetPasswordTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser> where TUser : IdentityUser<int>
    {
        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
             return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public override async Task<string> GetUserModifierAsync(string purpose, 
            UserManager<TUser> manager,
            TUser user)
        {
            return "Password:" + purpose + ":" + await manager.GetUserIdAsync(user).ConfigureAwait(false);
        }
    }
}

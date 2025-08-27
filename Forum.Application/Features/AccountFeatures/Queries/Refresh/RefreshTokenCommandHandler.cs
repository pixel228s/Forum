using Forum.Application.Common.Dtos.Auth.Responses;
using Forum.Application.Common.SecurityService;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Forum.Application.Features.AccountFeatures.Queries.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly UserManager<User> _userManager;

        public RefreshTokenCommandHandler(ITokenProvider tokenProvider, UserManager<User> userManager)
        {
            this._tokenProvider = tokenProvider;
            _userManager = userManager;
        }

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenProvider.GetClaimsPrincipal(request.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity!.Name!).ConfigureAwait(false);

            if (user == null
                || user.RefreshToken != request.RefreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new AppException();
            }
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var tokenDto = await _tokenProvider.CreateToken(user, roles).ConfigureAwait(false);
            user.RefreshToken = tokenDto.RefreshToken;
            return tokenDto;
        }
    }
}

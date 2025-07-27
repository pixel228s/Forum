using Forum.Application.Features.AccountFeatures.Queries.Login.Models;
using MediatR;

namespace Forum.Application.Features.AccountFeatures.Queries.Refresh
{
    public class RefreshTokenCommand : IRequest<TokenDto>
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}

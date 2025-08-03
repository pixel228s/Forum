using Forum.Application.Common.Dtos.Auth.Responses;
using MediatR;

namespace Forum.Application.Features.AccountFeatures.Queries.Login
{
    public record LoginQuery(string username, string password) : IRequest<TokenDto>;
}

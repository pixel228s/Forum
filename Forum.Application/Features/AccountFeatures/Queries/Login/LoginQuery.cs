using Forum.Application.Features.AccountFeatures.Queries.Login.Models;
using MediatR;

namespace Forum.Application.Features.AccountFeatures.Queries.Login
{
    public record LoginQuery(string username, string password) : IRequest<AuthResponse>;
}

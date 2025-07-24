using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Commands.Registration
{
    public record RegisterUserCommand(
        string UserName,
        string Password,
        string Email,
        string FirstName,
        string LastName,
        string? picUrl) : IRequest<IdentityResult>
    {
    }
}

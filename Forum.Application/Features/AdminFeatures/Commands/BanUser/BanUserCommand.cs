using MediatR;

namespace Forum.Application.Features.AdminFeatures.Commands.BanUser
{
    public record BanUserCommand(string username) : IRequest<Unit>;
}

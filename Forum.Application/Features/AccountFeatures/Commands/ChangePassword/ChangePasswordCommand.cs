using Forum.Application.Features.AccountFeatures.Commands.ChangePassword.Models;
using MediatR;

namespace Forum.Application.Features.AccountFeatures.Commands.ChangePassword
{
    public record ChangePasswordCommand(
        string CurrentPassword, 
        string NewPassword, 
        string Email) : IRequest<Response>;
}

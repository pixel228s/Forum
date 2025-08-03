using Forum.Application.Common.Dtos.Auth.Responses;
using MediatR;

namespace Forum.Application.Features.AccountFeatures.Commands.ChangePassword
{
    public record ChangePasswordCommand(
        string CurrentPassword, 
        string NewPassword, 
        string Email) : IRequest<ChangePasswordResponse>;
}

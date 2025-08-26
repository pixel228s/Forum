using Forum.Application.Common.Dtos.Auth.Responses;
using MediatR;

namespace Forum.Application.Features.AccountFeatures.Commands.ChangePassword
{
    public record ChangePasswordCommand : IRequest<ChangePasswordResponse>
    { 
        public string CurrentPassword { get; set; }
        public string RepeatPassword { get; set; }
        public string NewPassword { get; set; }
        public string Id { get; set; }
    }
}

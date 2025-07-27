using MediatR;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.SendOtp
{
    public class ForgotPasswordCommand : IRequest<Unit>
    {
        public required string Email { get; set; }
    }
}

using MediatR;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.NewPassword
{
    public class ResetPasswordCommand : IRequest<Unit>
    {
        public required string Email { get; set; }
        public required string ResetToken { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}

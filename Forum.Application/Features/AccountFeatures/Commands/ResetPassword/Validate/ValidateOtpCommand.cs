using MediatR;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.Validate
{
    public class ValidateOtpCommand : IRequest<string?>
    {
        public required string Email { get; set; }
        public required string Otp { get; set; }
    }
}

using Forum.Application.Exceptions.Models;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.Validate
{
    public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, string?>
    {
        private readonly UserManager<User> _userManager;

        public ValidateOtpCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string?> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
            if (user == null)
            {
                return null;
            }

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "ResetPassword", request.Otp)
                .ConfigureAwait(false);
            if (!isValid)
            {
                throw new AppException("invalid otp code");
            }

            var resetToken = await _userManager
                .GeneratePasswordResetTokenAsync(user)
                .ConfigureAwait(false);
            return resetToken;
        }
    }
}

using Forum.Application.Exceptions.Models;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.NewPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public ResetPasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                .ConfigureAwait(false);
            if (user == null)
            {
                return Unit.Value;
            }

            var result = await _userManager
                .ResetPasswordAsync(user, request.ResetToken, request.Password)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                string message = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new AppException(message);
            }  

            return Unit.Value;
        }
    }
}

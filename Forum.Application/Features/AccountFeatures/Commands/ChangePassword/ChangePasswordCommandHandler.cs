using Forum.Application.Exceptions;
using Forum.Application.Features.AccountFeatures.Commands.ChangePassword.Models;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response>
    {
        private readonly UserManager<User> _userManager;

        public ChangePasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
            if (user == null)
            {
                throw new ObjectNotFoundException();
            }

            bool isPasswordCorrect = await _userManager
                .CheckPasswordAsync(user, request.CurrentPassword)
                .ConfigureAwait(false);
            if (!isPasswordCorrect)
            {
                throw new AuthenticationException();
            }

            var result = await _userManager
                .ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword)
                .ConfigureAwait(false);
            if (result.Succeeded)
            {
                result.Succeeded.ToString();
                return new Response();
            }
            string message = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new AuthenticationException(message);
        }
    }
}

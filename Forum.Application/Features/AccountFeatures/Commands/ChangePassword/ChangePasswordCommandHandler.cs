using Forum.Application.Common.Dtos.Auth.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
    {
        private readonly UserManager<User> _userManager;

        public ChangePasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id).ConfigureAwait(false);
            if (user == null)
            {
                throw new ObjectNotFoundException();
            }

            bool isPasswordCorrect = await _userManager
                .CheckPasswordAsync(user, request.CurrentPassword)
                .ConfigureAwait(false);
            if (!isPasswordCorrect)
            {
                throw new AuthenticationException("Password is incorrect");
            }

            var result = await _userManager
                .ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword)
                .ConfigureAwait(false);
            if (result.Succeeded)
            {
                return new ChangePasswordResponse();
            }
            string message = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new AuthenticationException(message);
        }
    }
}

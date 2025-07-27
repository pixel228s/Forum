using Forum.Application.Common.EmailSender;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.SendOtp
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;

        public ForgotPasswordCommandHandler(
            IUserRepository userRepository, 
            IEmailSender emailSender,
            UserManager<User> userManager)
        {
            _emailSender = emailSender;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetUserByEmail(request.Email, cancellationToken);
            if (user == null)
            {
                return Unit.Value;
            }
            var otp = await _userManager
                .GenerateTwoFactorTokenAsync(user!, "ResetPassword");

            await _emailSender.SendEmailAsync(request.Email, subject: "Otp", otp);
            return Unit.Value; 
        }
    }
}

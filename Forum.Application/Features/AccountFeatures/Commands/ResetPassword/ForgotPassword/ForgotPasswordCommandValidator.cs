using FluentValidation;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.SendOtp
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator() 
        {
            RuleFor(c => c.Email)
              .NotEmpty().WithMessage("Your email can't be emoty.")
              .EmailAddress();
        }
    }
}

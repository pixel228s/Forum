using FluentValidation;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.Validate
{
    public class ValidateOtpCommandValidator : AbstractValidator<ValidateOtpCommand>
    {
        public ValidateOtpCommandValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email field can not be empty")
                .EmailAddress();
        }
    }
}

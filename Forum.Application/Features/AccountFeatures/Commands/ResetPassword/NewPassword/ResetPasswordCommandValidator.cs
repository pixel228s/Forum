using FluentValidation;

namespace Forum.Application.Features.AccountFeatures.Commands.ResetPassword.NewPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password can not be empty");

            RuleFor(x => x.ResetToken)
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Repeated password can not be empty");
        }
    }
}

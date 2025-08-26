using FluentValidation;

namespace Forum.Application.Features.AccountFeatures.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(p => p.CurrentPassword)
                .NotEmpty().WithMessage("password should not be left empty.");

            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("New password should not be left empty.");

            RuleFor(p => new { p.RepeatPassword, p.CurrentPassword })
                .Must(x => x.RepeatPassword == x.RepeatPassword)
                .WithMessage("New Password and current password should be equal");
        }
    }
}

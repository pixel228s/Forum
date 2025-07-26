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

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Your email can't be emoty.")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$")
                .WithMessage("Your email should be in correct format");
        }
    }
}

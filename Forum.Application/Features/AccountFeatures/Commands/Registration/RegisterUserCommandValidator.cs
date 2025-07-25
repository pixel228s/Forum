using FluentValidation;

namespace Forum.Application.Features.AccountFeatures.Commands.Registration
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .WithMessage("Username can not be empty.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Your password can not be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Your email can't be emoty.")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$")
                .WithMessage("Your email should be in correct format");

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Your first name can't be empty");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Your last name can't be empty");
        }
    }
}

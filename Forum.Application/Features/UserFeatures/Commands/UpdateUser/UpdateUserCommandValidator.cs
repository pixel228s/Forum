using FluentValidation;

namespace Forum.Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("This is not a valid email");

            RuleFor(x => x.UserName)
                .MaximumLength(100)
                .WithMessage("Maximum length limit exceeded");
        }
    }
}

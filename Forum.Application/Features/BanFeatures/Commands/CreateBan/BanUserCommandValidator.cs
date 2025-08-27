using FluentValidation;
using Forum.Application.Features.AdminFeatures.Commands.BanUser;

namespace Forum.Application.Features.AdminFeatures.Commands.CreateBan
{
    public class BanUserCommandValidator : AbstractValidator<BanUserCommand>
    {
        public BanUserCommandValidator() 
        {
            RuleFor(x => x.BanEndDate)
                .NotEmpty()
                .WithMessage("please input ban end date")
                .Must(x => x > DateTime.Now)
                .WithMessage("Ban End Date can not be in the past");

            RuleFor(x => x.BanReason)
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Ban reason must be at least 10 characters long")
                .MaximumLength(500)
                .WithMessage("Ban reason must not exceed 500 characters");
        }
    }
}

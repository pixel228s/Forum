using FluentValidation;

namespace Forum.Application.Features.AdminFeatures.Commands.UpdateBan
{
    public class UpdateBanCommandValidator : AbstractValidator<UpdateBanCommand>
    {
        public UpdateBanCommandValidator() 
        {
            RuleFor(x => x.BannedUntil)
               .Must(x => x == null || x > DateTime.Now)
               .WithMessage("Ban End Date can not be in the past");

            RuleFor(x => x.BanReason)
                .MinimumLength(10)
                .WithMessage("Ban reason must be at least 10 characters long")
                .MaximumLength(500)
                .WithMessage("Ban reason must not exceed 500 characters");
        }
    }
}

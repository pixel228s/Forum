using FluentValidation;

namespace Forum.Application.Features.PostFeatures.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator() 
        {
            RuleFor(x => x.Title)
               .MaximumLength(300)
               .WithMessage("Maximum length of title exceeded");

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(4000)
                .WithMessage("Maximum length of content exceeded");
        }
    }
}

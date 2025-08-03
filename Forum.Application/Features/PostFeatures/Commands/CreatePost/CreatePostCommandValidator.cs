using FluentValidation;

namespace Forum.Application.Features.PostFeatures.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator() 
        {
            RuleFor(x => x.Title)
                .MaximumLength(300)
                .WithMessage("Maximum length of title exceeded");

            RuleFor(x => x.post)
                .NotEmpty()
                .WithMessage("post content can not be empty")
                .MaximumLength(4000)
                .WithMessage("Maximum length of content exceeded");
        }
    }
}

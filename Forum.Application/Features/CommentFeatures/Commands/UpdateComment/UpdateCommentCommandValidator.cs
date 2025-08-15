
using FluentValidation;

namespace Forum.Application.Features.CommentFeatures.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator() 
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Please, provide comment content")
                .MaximumLength(3000)
                .WithMessage("Maximum limit exceeded");
        }
    }
}

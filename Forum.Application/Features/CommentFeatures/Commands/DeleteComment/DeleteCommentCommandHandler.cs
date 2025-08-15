using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.CommentFeatures.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository
                .GetCommentById(request.CommentId, false, cancellationToken).ConfigureAwait(false);

            if (comment == null)
            {
                throw new ObjectNotFoundException();
            }

            if (comment.UserId != request.UserId)
            {
                throw new ActionForbiddenException();
            }

            await _commentRepository.RemoveAsync(comment, cancellationToken);
            return Unit.Value;
        }
    }
}

using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.UserFeatures.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ITransactionFactory _transactionFactory;
        public DeleteUserCommandHandler(UserManager<User> userManager,
            IPostRepository postRepository,
            ICommentRepository commentRepository,
            ITransactionFactory transactionFactory)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _transactionFactory = transactionFactory;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId)
                .ConfigureAwait(false);
            if (user == null)
            {
                throw new ObjectNotFoundException();
            }

            var requestAuthor = await _userManager.FindByIdAsync(request.RequesterId)
                .ConfigureAwait(false);

            if (requestAuthor == null)
            {
                throw new ObjectNotFoundException();
            }

            if (requestAuthor.Id != user.Id && !requestAuthor.IsAdmin)
            {
                throw new ActionForbiddenException("This action is forbidden");
            }

            using var transaction = await _transactionFactory.OpenTransactionAsync(cancellationToken)
              .ConfigureAwait(false);
            try
            {
                await _commentRepository.DeleteUserComments(user.Id, cancellationToken)
                .ConfigureAwait(false);
                var result = await _userManager.DeleteAsync(user)
                    .ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    string message = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new AppException(message);
                }

                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw;
            }
            
            return Unit.Value;
        }
    }
}

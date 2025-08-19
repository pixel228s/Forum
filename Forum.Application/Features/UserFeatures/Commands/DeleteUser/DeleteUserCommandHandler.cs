using Forum.Application.Exceptions;
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
        public DeleteUserCommandHandler(UserManager<User> userManager,
            IPostRepository postRepository,
            ICommentRepository commentRepository)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
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

            await _commentRepository.DeleteUserComments(user.Id, cancellationToken)
                .ConfigureAwait(false);
            await _userManager.DeleteAsync(user)
                .ConfigureAwait(false);
            
            return Unit.Value;
        }
    }
}

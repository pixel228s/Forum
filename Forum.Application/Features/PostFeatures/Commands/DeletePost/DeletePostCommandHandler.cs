using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.PostFeatures.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;

        public DeletePostCommandHandler(IPostRepository postRepository,
            UserManager<User> userManager) 
        { 
            _postRepository = postRepository;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostByIdAsync(request.PostId, cancellationToken, false, false)
                .ConfigureAwait(false);

            if (post == null)
            {
                throw new ObjectNotFoundException("Post not found");
            }

            var user = await _userManager.FindByIdAsync(request.UserId).ConfigureAwait(false);

            if (user == null)
            {
                throw new ObjectNotFoundException("User not found");
            }

            if (post.UserId != user.Id && !user.IsAdmin)
            {
                throw new ActionForbiddenException();
            }

            await _postRepository.RemoveAsync(post, cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Forum.Application.Features.PostFeatures.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository, 
            IHttpContextAccessor httpContextAccessor) 
        { 
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostByIdAsync(request.PostId, cancellationToken, false, false);

            if (post == null)
            {
                throw new ObjectNotFoundException();
            }
   
            if (post.UserId != request.UserId)
            {
                throw new ActionForbiddenException();
            }

            await _postRepository.RemoveAsync(post, cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeletePostCommandHandler(IPostRepository postRepository, 
            IHttpContextAccessor httpContextAccessor) 
        { 
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostByIdAsync(request.PostId, cancellationToken, false, false);

            if (post == null)
            {
                throw new ObjectNotFoundException();
            }

            var userId = _httpContextAccessor
                .HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)!.Value;

            if (post.UserId != int.Parse(userId))
            {
                throw new ActionForbiddenException();
            }

            await _postRepository.RemoveAsync(post, cancellationToken);

            return Unit.Value;
        }
    }
}

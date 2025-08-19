using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Posts.Enums;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Commands.ChangeState
{
    public class ChangeStateCommandHandler : IRequestHandler<ChangeStateCommand, Unit>
    {

        private readonly IPostRepository _postRepository;

        public ChangeStateCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(ChangeStateCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository
                .GetPostByIdAsync(request.PostId, cancellationToken, false, true)
                .ConfigureAwait(false);

            if (post == null)
            {
                throw new ObjectNotFoundException();
            }

            post.State = request.IsAccepted ? State.Show : State.Hide;
            await _postRepository.UpdateEntity(post, cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

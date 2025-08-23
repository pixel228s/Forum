using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Entities.Posts.Enums;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(
            IPostRepository postRepository, 
            IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository
                .GetPostByIdAsync(request.Id, cancellationToken, false, false).ConfigureAwait(false);

            if (post == null)
            {
                throw new ObjectNotFoundException();
            }

            if (post.UserId != request.UserId!)
            {
                throw new ActionForbiddenException();
            }

            if (post.Status == Status.Inactive)
            {
                throw new AppException("Can't update an inactive post");
            }

            post.Title = request.Title ?? post.Title;
            post.Content = request.Content ?? post.Content;

            await _postRepository.UpdateEntity(post, cancellationToken).ConfigureAwait(false);

            return _mapper.Map<PostResponse>(post);
        }
    }
}

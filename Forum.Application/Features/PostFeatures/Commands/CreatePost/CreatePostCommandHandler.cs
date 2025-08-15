using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Posts;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(
            IPostRepository postRepository, 
            IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request);
            post.UserId = int.Parse(request.userId);

            await _postRepository.AddAsync(post, cancellationToken);

            return _mapper.Map<PostResponse>(post);
        }
    }
}

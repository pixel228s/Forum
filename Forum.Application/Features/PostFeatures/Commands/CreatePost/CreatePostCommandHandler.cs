using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Posts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Forum.Application.Features.PostFeatures.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(
            IPostRepository postRepository, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<PostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request);
            var userId = _httpContextAccessor
                .HttpContext?
                .User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            post.UserId = int.Parse(userId!);

            await _postRepository.AddAsync(post, cancellationToken);

            return _mapper.Map<PostResponse>(post);
        }
    }
}

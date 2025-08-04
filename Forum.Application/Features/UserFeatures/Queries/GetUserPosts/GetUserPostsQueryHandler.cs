using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.GetUserPosts
{
    public class GetUserPostsQueryHandler : IRequestHandler<GetUserPostsQuery, IEnumerable<PostResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetUserPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostResponse>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetPostsByUserId(request.Id, cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<PostResponse>>(posts);
        }
    }
}

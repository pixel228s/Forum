using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.GetAllPosts
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IPostRepository postRepository,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostResponse>> Handle(
            GetAllPostsQuery request,
            CancellationToken cancellationToken)
        {
            var posts = await _postRepository
                .GetAllPosts(cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<PostResponse>>(posts);
        }
    }
}

using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.RetrievePendingPosts
{
    public class GetPendingPostsQueryHandler : IRequestHandler<GetPendingPostsQuery, IEnumerable<PostResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public GetPendingPostsQueryHandler(IPostRepository postRepository,
            IMapper mapper) 
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostResponse>> Handle(GetPendingPostsQuery request, CancellationToken cancellationToken)
        {
            var pendingPosts = await _postRepository
                .GetPendingPosts(request.parameters, cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<PostResponse>>(pendingPosts);
        }
    }
}

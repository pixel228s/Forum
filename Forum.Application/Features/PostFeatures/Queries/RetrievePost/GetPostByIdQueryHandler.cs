using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.RetrievePost
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IPostRepository postRepository, 
            IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostResponse> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = 
                await _postRepository
                .GetPostByIdAsync(request.PostId, cancellationToken, true, false)
                .ConfigureAwait(false);

            if (post == null)
            {
                throw new ObjectNotFoundException("Post not found!");
            }

            return _mapper.Map<PostResponse>(post);
        }
    }
}

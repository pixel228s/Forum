using Forum.Domain.Entities.Posts;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.GetAllPosts
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostWithCommentCount>>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostsQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostWithCommentCount>> Handle(
            GetAllPostsQuery request,
            CancellationToken cancellationToken)
        {
            var posts = await _postRepository
                .GetAllPosts(cancellationToken)
                .ConfigureAwait(false);
            return posts;
        }
    }
}

using Forum.Application.Common.Dtos.Posts.Responses;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.RetrievePost
{
    public class GetPostByIdQuery : IRequest<PostResponse>
    {
        public int PostId { get; set; }
    }
}

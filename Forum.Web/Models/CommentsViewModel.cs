using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Application.Common.Dtos.Posts.Responses;

namespace Forum.Web.Models
{
    public class CommentsViewModel
    {
        public PostResponse Post { get; set; }
        public IEnumerable<CommentResponseDto> Comments { get; set; }
    }
}

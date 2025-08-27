using Forum.Application.Common.Dtos.Posts.Responses;

namespace Forum.Web.Models
{
    public class PendingPostsViewModel
    {
        public IEnumerable<PostResponse> PostResponses { get; set; }
        public int CurrentPage { get; set; }
    }
}

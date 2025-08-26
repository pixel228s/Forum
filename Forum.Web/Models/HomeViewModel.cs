using Forum.Application.Common.Dtos.Posts.Responses;

namespace Forum.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<PostResponse> Posts { get; set; }
    }
}

using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Application.Common.Dtos.Users.Responses;

namespace Forum.Web.Models
{
    public class ProfileViewModel
    {
        public UserResponse UserResponse { get; set; }
        public IEnumerable<PostResponse> UserPosts { get; set; }
    }
}

using Forum.Domain.Models.Posts;
using Microsoft.AspNetCore.Identity;

namespace Forum.Domain.Models.Users
{
    public class User : IdentityUser<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string? picUrl { get; set; }
        public bool IsBanned { get; set; }

        public Ban? BanInfo {  get; set; } 
        public ICollection<Post>? Posts { get; } = new List<Post>();
        public ICollection<Comment>? Comments { get; } = new List<Comment>();
    }
}

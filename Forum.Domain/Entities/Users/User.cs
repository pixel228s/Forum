using Forum.Domain.Entities.Comments;
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
        public bool IsAdmin { get; set; } = false!;
        public bool IsBanned { get; set; } = false!;

        public Ban? BanInfo {  get; set; } 
        public ICollection<Post>? Posts { get; } 
        public ICollection<Comment>? Comments { get; }
    }
}

using Forum.Domain.Models.Posts;
using Microsoft.AspNetCore.Identity;

namespace Forum.Domain.Models.Users
{
    public class User : IdentityUser<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string? picUrl { get; set; }
        public bool IsBanned { get; set; }

        public virtual Ban? BanInfo {  get; set; } 
        public virtual ICollection<Post>? Posts { get; } = new List<Post>();
        public virtual ICollection<Comment>? Comments { get; } = new List<Comment>();
    }
}

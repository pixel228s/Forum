using Forum.Domain.Models.Base;
using Forum.Domain.Models.Posts.Enums;
using Forum.Domain.Models.Users;

namespace Forum.Domain.Models.Posts
{
    public class Post : BaseEntity
    {
        public State State { get; set; } = State.Pending;
        public int UserId { get; set; }
        public string? picUrl { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment>? comments { get; set; }
    }
}

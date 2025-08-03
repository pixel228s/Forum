using Forum.Domain.Entities.Comments;
using Forum.Domain.Models.Base;
using Forum.Domain.Models.Posts.Enums;
using Forum.Domain.Models.Users;

namespace Forum.Domain.Models.Posts
{
    public class Post : BaseEntity
    {
        public State State { get; set; } = State.Pending;
        public int UserId { get; set; }
        public string? ImageUrl { get; set; }
        public string Content { get; set; }
        public string? Title { get; set; }

        public User User { get; set; }
        public ICollection<Comment>? comments { get; set; }
    }
}

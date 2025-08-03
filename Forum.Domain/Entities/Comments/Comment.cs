using Forum.Domain.Models.Base;
using Forum.Domain.Models.Posts;
using Forum.Domain.Models.Users;

namespace Forum.Domain.Entities.Comments
{
    public class Comment : BaseEntity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public required string Text { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}

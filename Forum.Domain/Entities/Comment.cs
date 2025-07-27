using Forum.Domain.Models.Base;
using Forum.Domain.Models.Posts;
using Forum.Domain.Models.Users;

namespace Forum.Domain.Models
{
    public class Comment : BaseEntity
    {
        public int UserId { get; set; } 
        public int PostId { get; set; }
        public required string Text { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}

using Forum.Domain.Models.Base;
using Forum.Domain.Models.Users;

namespace Forum.Domain.Models
{
    public class Ban : BaseEntity
    {
        public int UserId { get; set; }
        public required string BanReason { get; set; }
        public DateTime? BanEndDate { get; set; }
        public virtual User User { get; set; }
    }
}

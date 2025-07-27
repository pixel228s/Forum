using Microsoft.AspNetCore.Identity;

namespace Forum.Domain.Models.Users
{
    public class Role : IdentityRole<int>
    {
        public string? Description { get; set; }
    }
}

using Forum.Application.Common.Dtos.Users.Responses;

namespace Forum.Web.Models
{
    public class AllUsersViewModel
    {
        public IEnumerable<UserResponse> Responses { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

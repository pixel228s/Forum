namespace Forum.Application.Common.Dtos.Users.Requests
{
    public class UserUpdateRequest
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PfpUrl { get; set; }
    }
}

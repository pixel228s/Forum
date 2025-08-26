namespace Forum.Application.Common.Dtos.Auth.Requests
{
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string RepeatPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

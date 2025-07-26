namespace Forum.Application.Features.AccountFeatures.Commands.ChangePassword.Models
{
    public class Response
    {
        private readonly string SuccessMessage = "Password changed successfully";

        public string GetSuccessMessage => SuccessMessage;
    }
}

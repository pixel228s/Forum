namespace Forum.Application.Common.Dtos.Auth.Responses
{
    public class ChangePasswordResponse
    {
        private readonly string SuccessMessage = "Password changed successfully";

        public string GetSuccessMessage => SuccessMessage;
    }
}

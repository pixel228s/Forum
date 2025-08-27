using Forum.Application.Exceptions.Models;

namespace Forum.Application.Exceptions
{
    public class UserIsBannedException : AppException
    {
        public override int StatusCode { get; } = 403;
        public override string Title { get; } = "User is banned";

        public UserIsBannedException() { }
        public UserIsBannedException(string message) : base(message) { }
    }
}

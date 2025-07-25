using Forum.Application.Exceptions.Models;

namespace Forum.Application.Exceptions
{
    public class AuthenticationException : AppException
    {
        public override int StatusCode { get; } = 404;
        public override string Title { get; } = "User with this credentials do not exist";

        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }
    }
}

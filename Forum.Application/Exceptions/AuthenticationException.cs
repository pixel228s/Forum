using Forum.Application.Exceptions.Models;

namespace Forum.Application.Exceptions
{
    public class AuthenticationException : AppException
    {
        public override int StatusCode { get; } = 403;
        public override string Title { get; } = "Authentication failed.";

        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }
    }
}

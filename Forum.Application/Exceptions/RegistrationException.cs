using Forum.Application.Exceptions.Models;

namespace Forum.Application.Exceptions
{
    public class RegistrationException : AppException
    {
        public override int StatusCode { get; } = 409;
        public override string Title { get; } = "User could not register";

        public RegistrationException() { }
        public RegistrationException(string message) : base(message) { }
    }
}

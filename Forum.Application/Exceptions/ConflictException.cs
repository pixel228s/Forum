using Forum.Application.Exceptions.Models;

namespace Forum.Application.Exceptions
{
    public class ConflictException : AppException
    {
        public override int StatusCode { get; } = 409;
        public override string Title { get; } = "Conflict occured";

        public ConflictException() { }
        public ConflictException(string message) : base(message) { }
    }
}

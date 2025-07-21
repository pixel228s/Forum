using Forum.Application.Exceptions.Models;

namespace Forum.Application.Exceptions
{
    public class ObjectNotFoundException : AppException
    {
        public override int StatusCode { get; } = 404;
        public override string Title { get; } = "Object Has not been found";

        public ObjectNotFoundException() { }
        public ObjectNotFoundException(string message) : base(message) { }
    }
}

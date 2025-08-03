using Forum.Application.Exceptions.Models;

namespace Forum.Application.Exceptions
{
    public class ActionForbiddenException : AppException
    {
        public override int StatusCode { get; } = 403;
        public override string Title { get; } = "Action forbidden";

        public ActionForbiddenException() { }
        public ActionForbiddenException(string message) : base(message) { }
    }
}

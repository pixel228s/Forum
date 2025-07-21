using Forum.Application.Exceptions.Models;

namespace Forum.Application.Exceptions
{
    public class ActionNotAuthorizedException : AppException
    {
        public override int StatusCode { get; } = 401;
        public override string Title { get; } = "Action not authorized";

        public ActionNotAuthorizedException() { }
        public ActionNotAuthorizedException(string message) : base(message) { }
    }
}

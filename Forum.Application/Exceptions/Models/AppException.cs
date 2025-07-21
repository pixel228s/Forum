namespace Forum.Application.Exceptions.Models
{
    public class AppException : Exception
    {
        public virtual int StatusCode { get; } = 500;
        public virtual string Title { get; } = "Application Error Occured";

        public AppException() { }
        public AppException(string message) : base(message = "application exception") { }
    }
}

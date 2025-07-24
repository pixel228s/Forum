using FluentValidation;
using FluentValidation.Results;

namespace Forum.Application.Exceptions
{
    public class AppValidationException : ValidationException
    {
        public AppValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
        {
        }
    }
}

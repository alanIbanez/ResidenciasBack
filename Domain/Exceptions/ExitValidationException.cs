using Domain.Exceptions;

namespace Domain.Exceptions
{
    public class ExitValidationException : DomainException
    {
        public ExitValidationException(string message)
            : base(message, "EXIT_VALIDATION_ERROR", 400)
        {
        }

        public ExitValidationException(string message, Exception innerException)
            : base(message, "EXIT_VALIDATION_ERROR", 400, innerException)
        {
        }
    }
}
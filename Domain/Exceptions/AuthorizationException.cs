using Domain.Exceptions;

namespace Domain.Exceptions
{
    public class AuthorizationException : DomainException
    {
        public AuthorizationException(string message)
            : base(message, "AUTHORIZATION_ERROR", 403)
        {
        }

        public AuthorizationException(string message, Exception innerException)
            : base(message, "AUTHORIZATION_ERROR", 403, innerException)
        {
        }
    }
}
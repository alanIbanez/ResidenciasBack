using Domain.Exceptions;
using System;

namespace Domain.Exceptions
{
    public class ForbiddenException : DomainException
    {
        public ForbiddenException() : base("Access forbidden", "FORBIDDEN", 403)
        {
        }

        public ForbiddenException(string message) : base(message, "FORBIDDEN", 403)
        {
        }

        public ForbiddenException(string message, Exception innerException)
            : base(message, "FORBIDDEN", 403, innerException)
        {
        }
    }
}
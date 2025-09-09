using Domain.Exceptions;
using System;

namespace Domain.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException() : base("Unauthorized access", "UNAUTHORIZED", 401)
        {
        }

        public UnauthorizedException(string message) : base(message, "UNAUTHORIZED", 401)
        {
        }

        // CORRECCIÓN: Eliminar el constructor de 4 parámetros o ajustar DomainException
        public UnauthorizedException(string message, Exception innerException)
            : base($"{message} - {innerException?.Message}", "UNAUTHORIZED", 401)
        {
        }
    }
}
using Domain.Exceptions;
using System.Collections.Generic;

namespace Domain.Exceptions
{
    public class ValidationException : DomainException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException()
            : base("Validation failed", "VALIDATION_ERROR", 400)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Validation failed", "VALIDATION_ERROR", 400)
        {
            Errors = errors;
        }

        public ValidationException(string message, IDictionary<string, string[]> errors)
            : base(message, "VALIDATION_ERROR", 400)
        {
            Errors = errors;
        }

        public ValidationException(string field, string error)
            : base($"Validation failed for field: {field}", "VALIDATION_ERROR", 400)
        {
            Errors = new Dictionary<string, string[]>
            {
                [field] = new[] { error }
            };
        }
    }
}
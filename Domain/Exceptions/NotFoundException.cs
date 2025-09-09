using Domain.Exceptions;
using System;

namespace Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string resourceName, object resourceId)
            : base($"The {resourceName} with id {resourceId} was not found.", "NOT_FOUND", 404)
        {
        }

        public NotFoundException(string resourceName, string criteria)
            : base($"The {resourceName} with criteria {criteria} was not found.", "NOT_FOUND", 404)
        {
        }

        public NotFoundException(string message) : base(message, "NOT_FOUND", 404)
        {
        }
    }
}
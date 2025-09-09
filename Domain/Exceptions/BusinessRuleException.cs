using Domain.Exceptions;

namespace Domain.Exceptions
{
    public class BusinessRuleException : DomainException
    {
        public BusinessRuleException(string message)
            : base(message, "BUSINESS_RULE_ERROR", 422)
        {
        }

        public BusinessRuleException(string message, Exception innerException)
            : base(message, "BUSINESS_RULE_ERROR", 422, innerException)
        {
        }
    }
}
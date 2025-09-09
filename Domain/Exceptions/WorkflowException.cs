using Domain.Exceptions;

namespace Domain.Exceptions
{
    public class WorkflowException : DomainException
    {
        public WorkflowException(string message)
            : base(message, "WORKFLOW_ERROR", 409)
        {
        }

        public WorkflowException(string message, Exception innerException)
            : base(message, "WORKFLOW_ERROR", 409, innerException)
        {
        }

        public WorkflowException(string message, string currentStatus, string requiredStatus)
            : base($"{message}. Current status: {currentStatus}, Required: {requiredStatus}", "WORKFLOW_ERROR", 409)
        {
        }
    }
}
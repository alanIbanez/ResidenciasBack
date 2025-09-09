using Domain.Exceptions;

namespace Domain.Exceptions
{
    public class NotificationException : DomainException
    {
        public NotificationException(string message)
            : base(message, "NOTIFICATION_ERROR", 500)
        {
        }

        public NotificationException(string message, Exception innerException)
            : base(message, "NOTIFICATION_ERROR", 500, innerException)
        {
        }
    }
}
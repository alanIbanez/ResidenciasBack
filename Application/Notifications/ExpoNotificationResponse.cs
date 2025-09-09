namespace Application.Models.Notifications
{
    public class ExpoNotificationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TicketId { get; set; }
        public string[] Errors { get; set; }
    }
}
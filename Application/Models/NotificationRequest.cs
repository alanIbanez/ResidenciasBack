namespace Application.Models
{
    public class NotificationRequest
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public int TargetUserId { get; set; }
        public int? SourceUserId { get; set; }
        public object Data { get; set; }
    }
}
namespace Application.Models
{
    public class NotificationDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public int TargetUserId { get; set; }
        public int? SourceUserId { get; set; }
        public string SourceUserName { get; set; }
        public bool Read { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public object Data { get; set; }
    }
}
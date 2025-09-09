namespace Application.Models
{
    public class ExpoNotificationRequest
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public object Data { get; set; }
        public string Sound { get; set; } = "default";
        public int? Ttl { get; set; }
        public int? Expiration { get; set; }
        public string Priority { get; set; } = "default";
        public string ChannelId { get; set; } = "default";
    }
}
namespace API.ViewModel.Responses
{
    public class NotificationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? NotificationId { get; set; }
    }

    public class NotificationDetailResponse
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

    public class NotificationsListResponse
    {
        public List<NotificationDetailResponse> Notifications { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace API.ViewModel.Requests
{
    public class NotificationRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Target user ID is required")]
        public int TargetUserId { get; set; }

        public int? SourceUserId { get; set; }

        public object Data { get; set; }
    }
}
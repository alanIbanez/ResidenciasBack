using System.ComponentModel.DataAnnotations;

namespace API.ViewModel.Requests
{
    public class NotificationTokenRequest
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
    }
}
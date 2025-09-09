using System.ComponentModel.DataAnnotations;

namespace API.ViewModel.Requests
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
    }
}
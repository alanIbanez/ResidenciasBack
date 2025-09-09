using Domain.Entities.Core;

namespace Application.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public User User { get; set; }
    }
}
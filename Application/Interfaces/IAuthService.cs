using Application.Models;
using Application.Models.Auth;
using Domain.Entities.Core;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(LoginModel loginModel);
        Task<AuthResult> RegisterAsync(RegisterModel registerModel);
        Task<AuthResult> RefreshTokenAsync(RefreshTokenModel refreshTokenModel);
        Task<bool> RevokeTokenAsync(int userId);
        Task<User> ValidateTokenAsync(string token);
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
    }
}
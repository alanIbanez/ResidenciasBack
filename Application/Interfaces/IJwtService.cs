using Domain.Entities.Core;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal ValidateToken(string token);
        DateTime GetRefreshTokenExpiry();
    }
}
using Application.Interfaces;
using Application.Models;
using Application.Models.Auth;
using Domain.Entities.Core;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordService passwordService,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordService = passwordService;
            _logger = logger;
        }

        // CAMBIO: Ahora recibe LoginModel en lugar de parámetros individuales
        public async Task<AuthResult> AuthenticateAsync(LoginModel loginModel)
        {
            try
            {
                // CAMBIO: Usar loginModel.Username en lugar del parámetro string username
                var user = await _userRepository.GetByUsernameAsync(loginModel.Username);
                if (user == null || !user.Active)
                    return new AuthResult { Success = false, Message = "Invalid credentials" };

                // CAMBIO: Usar loginModel.Password
                if (!_passwordService.VerifyPassword(loginModel.Password, user.PasswordHash))
                    return new AuthResult { Success = false, Message = "Invalid credentials" };

                // Update last login
                user.LastLogin = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);

                var token = _jwtService.GenerateJwtToken(user);
                var refreshToken = _jwtService.GenerateRefreshToken();

                // Save refresh token to user
                user.NavigationToken = refreshToken;
                await _userRepository.UpdateAsync(user);

                return new AuthResult
                {
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    User = user
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error authenticating user");
                return new AuthResult { Success = false, Message = "Authentication failed" };
            }
        }

        // Este método ya estaba correcto (recibe RegisterModel)
        public async Task<AuthResult> RegisterAsync(RegisterModel registerModel)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _userRepository.GetByUsernameAsync(registerModel.Username);
                if (existingUser != null)
                    return new AuthResult { Success = false, Message = "Username already exists" };

                var existingEmail = await _userRepository.GetByEmailAsync(registerModel.Email);
                if (existingEmail != null)
                    return new AuthResult { Success = false, Message = "Email already exists" };

                // Create new user
                var user = new User
                {
                    Username = registerModel.Username,
                    PasswordHash = _passwordService.HashPassword(registerModel.Password),
                    RoleId = registerModel.RoleId,
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    Person = new Person
                    {
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        DNI = registerModel.DNI,
                        Email = registerModel.Email,
                        Phone = registerModel.Phone,
                        Address = registerModel.Address,
                        BirthDate = registerModel.BirthDate,
                        Active = true,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await _userRepository.AddAsync(user);

                var token = _jwtService.GenerateJwtToken(user);
                var refreshToken = _jwtService.GenerateRefreshToken();

                user.NavigationToken = refreshToken;
                await _userRepository.UpdateAsync(user);

                return new AuthResult
                {
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    User = user,
                    Message = "User registered successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                return new AuthResult { Success = false, Message = "Registration failed" };
            }
        }

        // CAMBIO: Nuevo método para aceptar RefreshTokenModel
        public async Task<AuthResult> RefreshTokenAsync(RefreshTokenModel refreshTokenModel)
        {
            try
            {
                // CAMBIO: Usar refreshTokenModel.Token
                var user = await _userRepository.GetByRefreshTokenAsync(refreshTokenModel.Token);
                if (user == null || !user.Active)
                    return new AuthResult { Success = false, Message = "Invalid refresh token" };

                var newToken = _jwtService.GenerateJwtToken(user);
                var newRefreshToken = _jwtService.GenerateRefreshToken();

                user.NavigationToken = newRefreshToken;
                await _userRepository.UpdateAsync(user);

                return new AuthResult
                {
                    Success = true,
                    Token = newToken,
                    RefreshToken = newRefreshToken,
                    User = user
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing token");
                return new AuthResult { Success = false, Message = "Token refresh failed" };
            }
        }

        // Métodos sin cambios
        public async Task<bool> RevokeTokenAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    user.NavigationToken = null;
                    await _userRepository.UpdateAsync(user);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking token");
                return false;
            }
        }

        public async Task<User> ValidateTokenAsync(string token)
        {
            try
            {
                var principal = _jwtService.ValidateToken(token);
                if (principal == null) return null;

                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return null;

                return await _userRepository.GetByIdAsync(userId);
            }
            catch
            {
                return null;
            }
        }

        // CAMBIO: Mantenemos este método por si se necesita internamente
        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !user.Active) return false;

            return _passwordService.VerifyPassword(password, user.PasswordHash);
        }
    }
}
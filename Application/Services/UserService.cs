using Application.Interfaces;
using Application.Interfaces;
using Application.Models;
using Domain.Entities.Core;
using Domain.Interfaces;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IPasswordService passwordService,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _logger = logger;
        }

        public async Task<User> GetUserProfileAsync(int userId)
        {
            try
            {
                return await _userRepository.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user profile");
                throw;
            }
        }

        public async Task<OperationResult> UpdateUserProfileAsync(int userId, UpdateProfileModel model)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    return new OperationResult { Success = false, Message = "User not found" };

                // Update person details
                user.Person.FirstName = model.FirstName ?? user.Person.FirstName;
                user.Person.LastName = model.LastName ?? user.Person.LastName;
                user.Person.Email = model.Email ?? user.Person.Email;
                user.Person.Phone = model.Phone ?? user.Person.Phone;
                user.Person.Address = model.Address ?? user.Person.Address;
                user.Person.BirthDate = model.BirthDate ?? user.Person.BirthDate;
                user.Person.ModifiedAt = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);

                return new OperationResult { Success = true, Message = "Profile updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user profile");
                return new OperationResult { Success = false, Message = "Profile update failed" };
            }
        }

        public async Task<OperationResult> ChangePasswordAsync(int userId, ChangePasswordModel model)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    return new OperationResult { Success = false, Message = "User not found" };

                if (!_passwordService.VerifyPassword(model.CurrentPassword, user.PasswordHash))
                    return new OperationResult { Success = false, Message = "Current password is incorrect" };

                user.PasswordHash = _passwordService.HashPassword(model.NewPassword);
                await _userRepository.UpdateAsync(user);

                return new OperationResult { Success = true, Message = "Password changed successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password");
                return new OperationResult { Success = false, Message = "Password change failed" };
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(int page = 1, int pageSize = 20)
        {
            try
            {
                return await _userRepository.GetAllAsync(page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                throw;
            }
        }

        public async Task<OperationResult> UpdateUserStatusAsync(int userId, bool isActive)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    return new OperationResult { Success = false, Message = "User not found" };

                user.Active = isActive;
                await _userRepository.UpdateAsync(user);

                return new OperationResult { Success = true, Message = $"User {(isActive ? "activated" : "deactivated")} successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user status");
                return new OperationResult { Success = false, Message = "Status update failed" };
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _userRepository.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by id");
                throw;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _userRepository.GetByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by username");
                throw;
            }
        }
    }
}
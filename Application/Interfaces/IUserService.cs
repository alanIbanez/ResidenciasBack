using Application.Models;
using Domain.Entities.Core;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserProfileAsync(int userId);
        Task<OperationResult> UpdateUserProfileAsync(int userId, UpdateProfileModel model);
        Task<OperationResult> ChangePasswordAsync(int userId, ChangePasswordModel model);
        Task<IEnumerable<User>> GetAllUsersAsync(int page = 1, int pageSize = 20);
        Task<OperationResult> UpdateUserStatusAsync(int userId, bool isActive);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
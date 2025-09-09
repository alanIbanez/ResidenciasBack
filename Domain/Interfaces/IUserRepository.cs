using Domain.Entities.Core;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
        Task<IEnumerable<User>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<int> GetCountAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> UserExistsAsync(string username, string email);
        Task UpdateNotificationTokenAsync(int userId, string token);
        Task<User> ValidateCredentialsAsync(string username, string password);
        Task<bool> IsUsernameAvailableAsync(string username, int? excludeUserId = null);
        Task<bool> IsEmailAvailableAsync(string email, int? excludeUserId = null);
    }
}
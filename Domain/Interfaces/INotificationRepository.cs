using Domain.Entities.Core;

namespace Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(int id);
        Task<IEnumerable<Notification>> GetByUserIdAsync(int userId, int page = 1, int pageSize = 20);
        Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId);
        Task<int> GetCountByUserIdAsync(int userId);
        Task<int> GetUnreadCountAsync(int userId);
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(int id);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int userId);
        Task<IEnumerable<Notification>> GetByTypeAsync(string type, int page = 1, int pageSize = 20);
        Task<IEnumerable<Notification>> GetRecentNotificationsAsync(int hours = 24);
    }
}
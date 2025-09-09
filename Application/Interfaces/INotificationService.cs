using Application.Models;
using Domain.Entities.Core;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResult> SendNotificationAsync(NotificationRequest notificationRequest);
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId, int page = 1, int pageSize = 20);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task MarkAsReadAsync(int notificationId);
        Task DeleteNotificationAsync(int notificationId);
        Task<int> GetUnreadCountAsync(int userId);
        Task SaveNotificationTokenAsync(int userId, string token);
    }
}
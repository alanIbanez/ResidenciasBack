using Domain.Entities.Core;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(AppDbContext context, ILogger<NotificationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications
                .Include(n => n.TargetUser)
                .Include(n => n.SourceUser)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId, int page = 1, int pageSize = 20)
        {
            return await _context.Notifications
                .Include(n => n.SourceUser)
                .ThenInclude(u => u.Person)
                .Where(n => n.TargetUserId == userId)
                .OrderByDescending(n => n.SentAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Include(n => n.SourceUser)
                .ThenInclude(u => u.Person)
                .Where(n => n.TargetUserId == userId && !n.Read)
                .OrderByDescending(n => n.SentAt)
                .ToListAsync();
        }

        public async Task<int> GetCountByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.TargetUserId == userId)
                .CountAsync();
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.TargetUserId == userId && !n.Read)
                .CountAsync();
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notification = await GetByIdAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await GetByIdAsync(notificationId);
            if (notification != null && !notification.Read)
            {
                notification.Read = true;
                notification.ReadAt = DateTime.UtcNow;
                await UpdateAsync(notification);
            }
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            var unreadNotifications = await _context.Notifications
                .Where(n => n.TargetUserId == userId && !n.Read)
                .ToListAsync();

            foreach (var notification in unreadNotifications)
            {
                notification.Read = true;
                notification.ReadAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetByTypeAsync(string type, int page = 1, int pageSize = 20)
        {
            return await _context.Notifications
                .Include(n => n.TargetUser)
                .Include(n => n.SourceUser)
                .ThenInclude(u => u.Person)
                .Where(n => n.Type == type)
                .OrderByDescending(n => n.SentAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetRecentNotificationsAsync(int hours = 24)
        {
            var cutoffDate = DateTime.UtcNow.AddHours(-hours);

            return await _context.Notifications
                .Include(n => n.TargetUser)
                .Include(n => n.SourceUser)
                .ThenInclude(u => u.Person)
                .Where(n => n.SentAt >= cutoffDate)
                .OrderByDescending(n => n.SentAt)
                .ToListAsync();
        }
    }
}
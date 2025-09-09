using Application.Interfaces;
using Application.Models;
using Domain.Entities.Core;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExpoNotificationService _expoNotificationService;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationRepository notificationRepository,
            IUserRepository userRepository,
            IExpoNotificationService expoNotificationService,
            ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _expoNotificationService = expoNotificationService;
            _logger = logger;
        }

        // CAMBIO: Ahora recibe NotificationRequest (Model) en lugar de parámetros individuales
        public async Task<NotificationResult> SendNotificationAsync(NotificationRequest notificationRequest)
        {
            try
            {
                // CAMBIO: Usar notificationRequest.TargetUserId
                var targetUser = await _userRepository.GetByIdAsync(notificationRequest.TargetUserId);
                if (targetUser == null || !targetUser.Active)
                    return new NotificationResult { Success = false, Message = "Target user not found or inactive" };

                // Send push notification if user has a token
                if (!string.IsNullOrEmpty(targetUser.NotificationToken))
                {
                    var expoRequest = new Application.Models.ExpoNotificationRequest
                    {
                        To = targetUser.NotificationToken,
                        Title = notificationRequest.Title,
                        Body = notificationRequest.Message,
                        Data = notificationRequest.Data
                    };

                    var expoResponse = await _expoNotificationService.SendPushNotificationAsync(expoRequest);

                    if (!expoResponse.Success)
                    {
                        _logger.LogWarning("Expo notification failed: {Message}", expoResponse.Message);
                    }
                }

                // Save notification to database
                var notification = new Notification
                {
                    Title = notificationRequest.Title,
                    Message = notificationRequest.Message,
                    Type = notificationRequest.Type,
                    TargetUserId = notificationRequest.TargetUserId,
                    SourceUserId = notificationRequest.SourceUserId,
                    SentAt = DateTime.UtcNow
                };

                await _notificationRepository.AddAsync(notification);

                return new NotificationResult
                {
                    Success = true,
                    Message = "Notification sent successfully",
                    NotificationId = notification.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification");
                return new NotificationResult { Success = false, Message = $"Internal error: {ex.Message}" };
            }
        }

        // Métodos sin cambios (devuelven Entities que serán mapeados en el Controller)
        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId, int page = 1, int pageSize = 20)
        {
            try
            {
                return await _notificationRepository.GetByUserIdAsync(userId, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user notifications");
                throw;
            }
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            try
            {
                return await _notificationRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting notification by id");
                throw;
            }
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            try
            {
                await _notificationRepository.MarkAsReadAsync(notificationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification as read");
                throw;
            }
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            try
            {
                await _notificationRepository.DeleteAsync(notificationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting notification");
                throw;
            }
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            try
            {
                return await _notificationRepository.GetUnreadCountAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread count");
                throw;
            }
        }

        public async Task SaveNotificationTokenAsync(int userId, string token)
        {
            try
            {
                await _userRepository.UpdateNotificationTokenAsync(userId, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving notification token");
                throw;
            }
        }
    }
}
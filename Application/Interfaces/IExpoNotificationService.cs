using Application.Models;

namespace Application.Interfaces
{
    public interface IExpoNotificationService
    {
        Task<ExpoNotificationResponse> SendPushNotificationAsync(ExpoNotificationRequest request);
        Task<bool> ValidateExpoTokenAsync(string token);
    }
}
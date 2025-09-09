using Application.Interfaces;
using Application.Notifications;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExpoNotificationService, ExpoNotificationService>();

            // AutoMapper se configura en API layer
            return services;
        }
    }
}
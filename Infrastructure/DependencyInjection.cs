using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Context con Npgsql
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                        npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    }));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExitRepository, ExitRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IResidentRepository, ResidentRepository>();
            services.AddScoped<ITutorRepository, TutorRepository>();
            services.AddScoped<IPreceptorRepository, PreceptorRepository>();
            services.AddScoped<IGuardRepository, GuardRepository>();
            services.AddScoped<IResidentTypeRepository, ResidentTypeRepository>();
            services.AddScoped<IPreceptorTypeRepository, PreceptorTypeRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<IExitTypeRepository, ExitTypeRepository>();
            services.AddScoped<IExitStatusRepository, ExitStatusRepository>();

            // Services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordService, PasswordService>();

            return services;
        }
    }
}
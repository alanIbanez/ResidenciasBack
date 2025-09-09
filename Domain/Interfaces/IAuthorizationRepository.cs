using Domain.Entities.Exit;

namespace Domain.Interfaces
{
    public interface IAuthorizationRepository
    {
        Task<ExitAuthorization> GetByIdAsync(int id);
        Task<IEnumerable<ExitAuthorization>> GetByExitIdAsync(int exitId);
        Task<IEnumerable<ExitAuthorization>> GetPendingByRoleAsync(string role);
        Task AddAsync(ExitAuthorization authorization);
        Task UpdateAsync(ExitAuthorization authorization);
        Task<bool> AuthorizationExistsAsync(int exitId, string role, int? userId);
    }
}
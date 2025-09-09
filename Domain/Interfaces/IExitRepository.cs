using Domain.Entities.Exit;

namespace Domain.Interfaces
{
    public interface IExitRepository
    {
        Task<Exit> GetByIdAsync(int id);
        Task<Exit> GetByTokenAsync(string exitToken);
        Task<IEnumerable<Exit>> GetByResidentIdAsync(int residentId);
        Task<IEnumerable<Exit>> GetByStatusAsync(int exitStatusId);
        Task<IEnumerable<Exit>> GetPendingAuthorizationsAsync();
        Task AddAsync(Exit exit);
        Task UpdateAsync(Exit exit);
        Task UpdateStatusAsync(int exitId, int exitStatusId);
        Task<bool> ExitExistsAsync(int exitId);
    }
}
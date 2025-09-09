using Domain.Entities.Exit;

namespace Domain.Interfaces
{
    public interface IExitStatusRepository
    {
        Task<ExitStatus> GetByIdAsync(int id);
        Task<ExitStatus> GetByNameAsync(string name);
        Task<IEnumerable<ExitStatus>> GetAllAsync();
        Task AddAsync(ExitStatus exitStatus);
        Task UpdateAsync(ExitStatus exitStatus);
        Task DeleteAsync(int id);
        Task<bool> ExitStatusExistsAsync(string name);
    }
}
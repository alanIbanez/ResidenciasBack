using BackendNotificaciones.Domain.Entities.Exit;
using Domain.Entities.Exit;

namespace Domain.Interfaces
{
    public interface IExitStatusRepository
    {
        Task<ExitStatus> GetByIdAsync(Guid id);
        Task<ExitStatus> GetByNameAsync(string name);
        Task<IEnumerable<ExitStatus>> GetAllAsync();
        Task AddAsync(ExitStatus exitStatus);
        Task UpdateAsync(ExitStatus exitStatus);
        Task DeleteAsync(ExitStatus exitStatus);
        Task SaveChangesAsync();
    }
}
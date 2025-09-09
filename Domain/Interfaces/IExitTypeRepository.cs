using Domain.Entities.Exit;

namespace Domain.Interfaces
{
    public interface IExitTypeRepository
    {
        Task<ExitType> GetByIdAsync(Guid id);
        Task<ExitType> GetByNameAsync(string name);
        Task<IEnumerable<ExitType>> GetAllAsync();
        Task AddAsync(ExitType exitType);
        Task UpdateAsync(ExitType exitType);
        Task DeleteAsync(ExitType exitType);
        Task SaveChangesAsync();
    }
}
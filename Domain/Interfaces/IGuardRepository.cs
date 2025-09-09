using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IGuardRepository
    {
        Task<Guard> GetByIdAsync(Guid id);
        Task<Guard> GetByPersonIdAsync(Guid personId);
        Task<IEnumerable<Guard>> GetByShiftIdAsync(Guid shiftId);
        Task<IEnumerable<Guard>> GetAllAsync();
        Task AddAsync(Guard guard);
        Task UpdateAsync(Guard guard);
        Task DeleteAsync(Guard guard);
        Task SaveChangesAsync();
    }
}
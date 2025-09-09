using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IShiftRepository
    {
        Task<Shift> GetByIdAsync(Guid id);
        Task<Shift> GetByNameAsync(string name);
        Task<IEnumerable<Shift>> GetAllAsync();
        Task AddAsync(Shift shift);
        Task UpdateAsync(Shift shift);
        Task DeleteAsync(Shift shift);
        Task SaveChangesAsync();
    }
}
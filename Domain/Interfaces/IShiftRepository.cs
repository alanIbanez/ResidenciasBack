using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IShiftRepository
    {
        Task<Shift> GetByIdAsync(int id);
        Task<Shift> GetByNameAsync(string name);
        Task<IEnumerable<Shift>> GetAllAsync();
        Task AddAsync(Shift shift);
        Task UpdateAsync(Shift shift);
        Task DeleteAsync(int id);
        Task<bool> ShiftExistsAsync(string name);
    }
}
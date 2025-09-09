using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IResidentTypeRepository
    {
        Task<ResidentType> GetByIdAsync(Guid id);
        Task<ResidentType> GetByNameAsync(string name);
        Task<IEnumerable<ResidentType>> GetAllAsync();
        Task AddAsync(ResidentType residentType);
        Task UpdateAsync(ResidentType residentType);
        Task DeleteAsync(ResidentType residentType);
        Task SaveChangesAsync();
    }
}
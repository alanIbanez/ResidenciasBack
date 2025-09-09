using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IResidentTypeRepository
    {
        Task<ResidentType> GetByIdAsync(int id);
        Task<ResidentType> GetByNameAsync(string name);
        Task<IEnumerable<ResidentType>> GetAllAsync();
        Task AddAsync(ResidentType residentType);
        Task UpdateAsync(ResidentType residentType);
        Task DeleteAsync(int id);
        Task<bool> ResidentTypeExistsAsync(string name);
    }
}
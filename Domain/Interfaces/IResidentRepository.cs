using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IResidentRepository
    {
        Task<Resident> GetByIdAsync(Guid id);
        Task<Resident> GetByPersonIdAsync(Guid personId);
        Task<IEnumerable<Resident>> GetByTutorIdAsync(Guid tutorId);
        Task<IEnumerable<Resident>> GetByResidentTypeIdAsync(Guid residentTypeId);
        Task<IEnumerable<Resident>> GetAllAsync();
        Task AddAsync(Resident resident);
        Task UpdateAsync(Resident resident);
        Task DeleteAsync(Resident resident);
        Task SaveChangesAsync();
    }
}
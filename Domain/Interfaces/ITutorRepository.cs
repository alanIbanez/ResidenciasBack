using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface ITutorRepository
    {
        Task<Tutor> GetByIdAsync(Guid id);
        Task<Tutor> GetByPersonIdAsync(Guid personId);
        Task<IEnumerable<Tutor>> GetAllAsync();
        Task AddAsync(Tutor tutor);
        Task UpdateAsync(Tutor tutor);
        Task DeleteAsync(Tutor tutor);
        Task SaveChangesAsync();
    }
}
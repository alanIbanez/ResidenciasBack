using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IPreceptorRepository
    {
        Task<Preceptor> GetByIdAsync(Guid id);
        Task<Preceptor> GetByPersonIdAsync(Guid personId);
        Task<IEnumerable<Preceptor>> GetByPreceptorTypeIdAsync(Guid preceptorTypeId);
        Task<IEnumerable<Preceptor>> GetByShiftIdAsync(Guid shiftId);
        Task<IEnumerable<Preceptor>> GetAllAsync();
        Task AddAsync(Preceptor preceptor);
        Task UpdateAsync(Preceptor preceptor);
        Task DeleteAsync(Preceptor preceptor);
        Task SaveChangesAsync();
    }
}
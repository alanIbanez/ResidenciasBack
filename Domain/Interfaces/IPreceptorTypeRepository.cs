using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IPreceptorTypeRepository
    {
        Task<PreceptorType> GetByIdAsync(int id);
        Task<PreceptorType> GetByNameAsync(string name);
        Task<IEnumerable<PreceptorType>> GetAllAsync();
        Task AddAsync(PreceptorType preceptorType);
        Task UpdateAsync(PreceptorType preceptorType);
        Task DeleteAsync(int id);
        Task<bool> PreceptorTypeExistsAsync(string name);
    }
}
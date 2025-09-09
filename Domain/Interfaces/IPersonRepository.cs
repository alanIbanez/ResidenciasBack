using Domain.Entities.Core;

namespace Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<Person> GetByDniAsync(string dni);
        Task<Person> GetByEmailAsync(string email);
        Task<IEnumerable<Person>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<int> GetCountAsync();
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
        Task<bool> PersonExistsAsync(string dni, string email);
        Task<bool> IsDniAvailableAsync(string dni, int? excludePersonId = null);
        Task<bool> IsEmailAvailableAsync(string email, int? excludePersonId = null);
    }
}
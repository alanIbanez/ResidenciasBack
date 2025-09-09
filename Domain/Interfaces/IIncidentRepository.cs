using Domain.Entities.Incident;

namespace Domain.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident> GetByIdAsync(int id);
        Task<IEnumerable<Incident>> GetByResidentIdAsync(int residentId);
        Task<IEnumerable<Incident>> GetByPreceptorIdAsync(int preceptorId);
        Task<IEnumerable<Incident>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Incident>> GetBySeverityAsync(string severity);
        Task AddAsync(Incident incident);
        Task UpdateAsync(Incident incident);
        Task DeleteAsync(int id);
    }
}
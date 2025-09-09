using Domain.Entities.Academic;

namespace Domain.Interfaces
{
    public interface IAttendanceTypeRepository
    {
        Task<AttendanceType> GetByIdAsync(int id);
        Task<AttendanceType> GetByNameAsync(string name);
        Task<IEnumerable<AttendanceType>> GetAllAsync();
        Task AddAsync(AttendanceType attendanceType);
        Task UpdateAsync(AttendanceType attendanceType);
        Task DeleteAsync(int id);
        Task<bool> AttendanceTypeExistsAsync(string name);
    }
}
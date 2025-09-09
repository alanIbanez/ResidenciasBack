using Domain.Entities.Event;

namespace Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<Attendance> GetByIdAsync(int id);
        Task<IEnumerable<Attendance>> GetByResidentIdAsync(int residentId);
        Task<IEnumerable<Attendance>> GetByEventIdAsync(int eventId);
        Task<IEnumerable<Attendance>> GetByDateAsync(DateTime date);
        Task AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task MarkPresentAsync(int attendanceId, bool present, string justification = null);
        Task<bool> AttendanceExistsAsync(int residentId, DateTime date, int? eventId = null);
    }
}
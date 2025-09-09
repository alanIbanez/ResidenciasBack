using Domain.Entities.Academic;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly AppDbContext _context;

        public ResidentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Resident> GetByIdAsync(Guid id)
        {
            return await _context.Residents
                .Include(r => r.Person)
                .Include(r => r.ResidentType)
                .Include(r => r.Tutor)
                .Include(r => r.Exits)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Resident> GetByPersonIdAsync(Guid personId)
        {
            return await _context.Residents
                .Include(r => r.Person)
                .Include(r => r.ResidentType)
                .Include(r => r.Tutor)
                .FirstOrDefaultAsync(r => r.PersonId == personId);
        }

        public async Task<IEnumerable<Resident>> GetByTutorIdAsync(Guid tutorId)
        {
            return await _context.Residents
                .Include(r => r.Person)
                .Include(r => r.ResidentType)
                .Where(r => r.TutorId == tutorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resident>> GetByResidentTypeIdAsync(Guid residentTypeId)
        {
            return await _context.Residents
                .Include(r => r.Person)
                .Include(r => r.ResidentType)
                .Where(r => r.ResidentTypeId == residentTypeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resident>> GetAllAsync()
        {
            return await _context.Residents
                .Include(r => r.Person)
                .Include(r => r.ResidentType)
                .Include(r => r.Tutor)
                .ToListAsync();
        }

        public async Task AddAsync(Resident resident)
        {
            await _context.Residents.AddAsync(resident);
        }

        public void Update(Resident resident)
        {
            _context.Residents.Update(resident);
        }

        public void Delete(Resident resident)
        {
            _context.Residents.Remove(resident);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
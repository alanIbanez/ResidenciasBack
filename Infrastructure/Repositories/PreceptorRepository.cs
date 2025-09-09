using Domain.Entities.Academic;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PreceptorRepository : IPreceptorRepository
    {
        private readonly AppDbContext _context;

        public PreceptorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Preceptor> GetByIdAsync(Guid id)
        {
            return await _context.Preceptors
                .Include(p => p.Person)
                .Include(p => p.PreceptorType)
                .Include(p => p.Shift)
                .Include(p => p.Events)
                .Include(p => p.Attendances)
                .Include(p => p.Incidents)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Preceptor> GetByPersonIdAsync(Guid personId)
        {
            return await _context.Preceptors
                .Include(p => p.Person)
                .Include(p => p.PreceptorType)
                .Include(p => p.Shift)
                .FirstOrDefaultAsync(p => p.PersonId == personId);
        }

        public async Task<IEnumerable<Preceptor>> GetByPreceptorTypeIdAsync(Guid preceptorTypeId)
        {
            return await _context.Preceptors
                .Include(p => p.Person)
                .Include(p => p.PreceptorType)
                .Where(p => p.PreceptorTypeId == preceptorTypeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Preceptor>> GetByShiftIdAsync(Guid shiftId)
        {
            return await _context.Preceptors
                .Include(p => p.Person)
                .Include(p => p.PreceptorType)
                .Where(p => p.ShiftId == shiftId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Preceptor>> GetAllAsync()
        {
            return await _context.Preceptors
                .Include(p => p.Person)
                .Include(p => p.PreceptorType)
                .Include(p => p.Shift)
                .ToListAsync();
        }

        public async Task AddAsync(Preceptor preceptor)
        {
            await _context.Preceptors.AddAsync(preceptor);
        }

        public void Update(Preceptor preceptor)
        {
            _context.Preceptors.Update(preceptor);
        }

        public void Delete(Preceptor preceptor)
        {
            _context.Preceptors.Remove(preceptor);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
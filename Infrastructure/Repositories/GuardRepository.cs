using Domain.Entities.Academic;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GuardRepository : IGuardRepository
    {
        private readonly AppDbContext _context;

        public GuardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guard> GetByIdAsync(Guid id)
        {
            return await _context.Guards
                .Include(g => g.Person)
                .Include(g => g.Shift)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Guard> GetByPersonIdAsync(Guid personId)
        {
            return await _context.Guards
                .Include(g => g.Person)
                .Include(g => g.Shift)
                .FirstOrDefaultAsync(g => g.PersonId == personId);
        }

        public async Task<IEnumerable<Guard>> GetByShiftIdAsync(Guid shiftId)
        {
            return await _context.Guards
                .Include(g => g.Person)
                .Include(g => g.Shift)
                .Where(g => g.ShiftId == shiftId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Guard>> GetAllAsync()
        {
            return await _context.Guards
                .Include(g => g.Person)
                .Include(g => g.Shift)
                .ToListAsync();
        }

        public async Task AddAsync(Guard guard)
        {
            await _context.Guards.AddAsync(guard);
        }

        public void Update(Guard guard)
        {
            _context.Guards.Update(guard);
        }

        public void Delete(Guard guard)
        {
            _context.Guards.Remove(guard);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
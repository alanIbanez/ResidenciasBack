using Domain.Entities.Academic;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly AppDbContext _context;

        public ShiftRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> GetByIdAsync(Guid id)
        {
            return await _context.Shifts
                .Include(s => s.Preceptors)
                .Include(s => s.Guards)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Shift> GetByNameAsync(string name)
        {
            return await _context.Shifts
                .FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<IEnumerable<Shift>> GetAllAsync()
        {
            return await _context.Shifts
                .Include(s => s.Preceptors)
                .Include(s => s.Guards)
                .ToListAsync();
        }

        public async Task AddAsync(Shift shift)
        {
            await _context.Shifts.AddAsync(shift);
        }

        public void Update(Shift shift)
        {
            _context.Shifts.Update(shift);
        }

        public void Delete(Shift shift)
        {
            _context.Shifts.Remove(shift);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
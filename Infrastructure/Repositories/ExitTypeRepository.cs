using Domain.Entities.Exit;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExitTypeRepository : IExitTypeRepository
    {
        private readonly AppDbContext _context;

        public ExitTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExitType> GetByIdAsync(Guid id)
        {
            return await _context.ExitTypes
                .Include(et => et.Exits)
                .FirstOrDefaultAsync(et => et.Id == id);
        }

        public async Task<ExitType> GetByNameAsync(string name)
        {
            return await _context.ExitTypes
                .FirstOrDefaultAsync(et => et.Name == name);
        }

        public async Task<IEnumerable<ExitType>> GetAllAsync()
        {
            return await _context.ExitTypes
                .Include(et => et.Exits)
                .ToListAsync();
        }

        public async Task AddAsync(ExitType exitType)
        {
            await _context.ExitTypes.AddAsync(exitType);
        }

        public void Update(ExitType exitType)
        {
            _context.ExitTypes.Update(exitType);
        }

        public void Delete(ExitType exitType)
        {
            _context.ExitTypes.Remove(exitType);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
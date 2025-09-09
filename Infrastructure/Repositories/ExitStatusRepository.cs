using BackendNotificaciones.Domain.Entities.Exit;
using Domain.Entities.Exit;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExitStatusRepository : IExitStatusRepository
    {
        private readonly AppDbContext _context;

        public ExitStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExitStatus> GetByIdAsync(Guid id)
        {
            return await _context.ExitStatuses
                .Include(es => es.Exits)
                .FirstOrDefaultAsync(es => es.Id == id);
        }

        public async Task<ExitStatus> GetByNameAsync(string name)
        {
            return await _context.ExitStatuses
                .FirstOrDefaultAsync(es => es.Name == name);
        }

        public async Task<IEnumerable<ExitStatus>> GetAllAsync()
        {
            return await _context.ExitStatuses
                .Include(es => es.Exits)
                .ToListAsync();
        }

        public async Task AddAsync(ExitStatus exitStatus)
        {
            await _context.ExitStatuses.AddAsync(exitStatus);
        }

        public void Update(ExitStatus exitStatus)
        {
            _context.ExitStatuses.Update(exitStatus);
        }

        public void Delete(ExitStatus exitStatus)
        {
            _context.ExitStatuses.Remove(exitStatus);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
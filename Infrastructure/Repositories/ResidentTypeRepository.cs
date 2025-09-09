using Domain.Entities.Academic;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ResidentTypeRepository : IResidentTypeRepository
    {
        private readonly AppDbContext _context;

        public ResidentTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResidentType> GetByIdAsync(Guid id)
        {
            return await _context.ResidentTypes
                .Include(rt => rt.Residents)
                .FirstOrDefaultAsync(rt => rt.Id == id);
        }

        public async Task<ResidentType> GetByNameAsync(string name)
        {
            return await _context.ResidentTypes
                .FirstOrDefaultAsync(rt => rt.Name == name);
        }

        public async Task<IEnumerable<ResidentType>> GetAllAsync()
        {
            return await _context.ResidentTypes
                .Include(rt => rt.Residents)
                .ToListAsync();
        }

        public async Task AddAsync(ResidentType residentType)
        {
            await _context.ResidentTypes.AddAsync(residentType);
        }

        public void Update(ResidentType residentType)
        {
            _context.ResidentTypes.Update(residentType);
        }

        public void Delete(ResidentType residentType)
        {
            _context.ResidentTypes.Remove(residentType);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
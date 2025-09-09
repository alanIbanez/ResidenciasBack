using Domain.Entities.Academic;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PreceptorTypeRepository : IPreceptorTypeRepository
    {
        private readonly AppDbContext _context;

        public PreceptorTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PreceptorType> GetByIdAsync(Guid id)
        {
            return await _context.PreceptorTypes
                .Include(pt => pt.Preceptors)
                .FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task<PreceptorType> GetByNameAsync(string name)
        {
            return await _context.PreceptorTypes
                .FirstOrDefaultAsync(pt => pt.Name == name);
        }

        public async Task<IEnumerable<PreceptorType>> GetAllAsync()
        {
            return await _context.PreceptorTypes
                .Include(pt => pt.Preceptors)
                .ToListAsync();
        }

        public async Task AddAsync(PreceptorType preceptorType)
        {
            await _context.PreceptorTypes.AddAsync(preceptorType);
        }

        public void Update(PreceptorType preceptorType)
        {
            _context.PreceptorTypes.Update(preceptorType);
        }

        public void Delete(PreceptorType preceptorType)
        {
            _context.PreceptorTypes.Remove(preceptorType);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
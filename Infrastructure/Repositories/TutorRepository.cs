using Domain.Entities.Academic;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private readonly AppDbContext _context;

        public TutorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tutor> GetByIdAsync(Guid id)
        {
            return await _context.Tutors
                .Include(t => t.Person)
                .Include(t => t.Residents)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tutor> GetByPersonIdAsync(Guid personId)
        {
            return await _context.Tutors
                .Include(t => t.Person)
                .Include(t => t.Residents)
                .FirstOrDefaultAsync(t => t.PersonId == personId);
        }

        public async Task<IEnumerable<Tutor>> GetAllAsync()
        {
            return await _context.Tutors
                .Include(t => t.Person)
                .Include(t => t.Residents)
                .ToListAsync();
        }

        public async Task AddAsync(Tutor tutor)
        {
            await _context.Tutors.AddAsync(tutor);
        }

        public void Update(Tutor tutor)
        {
            _context.Tutors.Update(tutor);
        }

        public void Delete(Tutor tutor)
        {
            _context.Tutors.Remove(tutor);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
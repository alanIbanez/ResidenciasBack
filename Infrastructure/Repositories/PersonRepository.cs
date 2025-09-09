using Domain.Entities.Core;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(AppDbContext context, ILogger<PersonRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.Persons
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person> GetByDniAsync(string dni)
        {
            return await _context.Persons
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.DNI == dni);
        }

        public async Task<Person> GetByEmailAsync(string email)
        {
            return await _context.Persons
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IEnumerable<Person>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _context.Persons
                .Include(p => p.User)
                .Where(p => p.Active)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Persons
                .Where(p => p.Active)
                .CountAsync();
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var person = await GetByIdAsync(id);
            if (person != null)
            {
                person.Active = false;
                await UpdateAsync(person);
            }
        }

        public async Task<bool> PersonExistsAsync(string dni, string email)
        {
            return await _context.Persons
                .AnyAsync(p => p.DNI == dni || p.Email == email);
        }

        public async Task<bool> IsDniAvailableAsync(string dni, int? excludePersonId = null)
        {
            var query = _context.Persons.Where(p => p.DNI == dni);

            if (excludePersonId.HasValue)
            {
                query = query.Where(p => p.Id != excludePersonId.Value);
            }

            return !await query.AnyAsync();
        }

        public async Task<bool> IsEmailAvailableAsync(string email, int? excludePersonId = null)
        {
            var query = _context.Persons.Where(p => p.Email == email);

            if (excludePersonId.HasValue)
            {
                query = query.Where(p => p.Id != excludePersonId.Value);
            }

            return !await query.AnyAsync();
        }
    }
}
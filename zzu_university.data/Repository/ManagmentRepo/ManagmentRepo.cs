using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model;
using zzu_university.data.Repository.MainRepo;

namespace zzu_university.data.Repository.ManagmentRepo
{
    public class ManagmentRepo : Repo<Management, int>, IManagmentRepo
    {
        private readonly ApplicationDbContext _context;

        public ManagmentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Get all with included ManagementType
        public async Task<IEnumerable<Management>> GetAllWithTypeAsync()
        {
            return await _context.Managments
                .Include(m => m.ManagementType)
                .ToListAsync();
        }

        // Get one by ID with ManagementType
        public async Task<Management?> GetWithTypeByIdAsync(int id)
        {
            return await _context.Managments
                .Include(m => m.ManagementType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Get first/default (optional use-case)
        public async Task<Management?> GetDefaultAsync()
        {
            return await _context.Managments
                .Include(m => m.ManagementType)
                .FirstOrDefaultAsync();
        }

        // Add new Management
        public async Task AddAsync(Management management)
        {
            await _context.Managments.AddAsync(management);
            await _context.SaveChangesAsync();
        }

        // Update by ID
        public async Task UpdateAsyncById(int id, Management updatedManagement)
        {
            var existing = await _context.Managments.FindAsync(id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updatedManagement);
                await _context.SaveChangesAsync();
            }
        }

        // Delete by ID
        public async Task DeleteAsyncById(int id)
        {
            var entity = await _context.Managments.FindAsync(id);
            if (entity != null)
            {
                _context.Managments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Legacy method for GetById
        public async Task<Management?> GetAsyncById(int id)
        {
            return await _context.Managments.FindAsync(id);
        }

        // Legacy method for first item
        public async Task<Management?> GetAsync()
        {
            return await _context.Managments.FirstOrDefaultAsync();
        }
    }
}

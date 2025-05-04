using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model;
using zzu_university.data.Repository.MainRepo;

namespace zzu_university.data.Repository.ManagmentRepo
{
    public class ManagmentRepo : Repo<Managment, int>, IManagmentRepo
    {
        private readonly ApplicationDbContext _context;

        public ManagmentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Returns the first Managment entity
        public async Task<Managment> GetAsync()
        {
            return await _context.Managments.FirstOrDefaultAsync();
        }

        // Returns a Managment entity by ID
        public async Task<Managment> GetAsyncById(int id)
        {
            return await _context.Managments.FindAsync(id);
        }

        // Adds a new Managment entity
        public async Task AddAsync(Managment managment)
        {
            await _context.Managments.AddAsync(managment);
        }

        // Updates a Managment entity by ID
        public async Task UpdateAsyncById(int id, Managment managment)
        {
            var existing = await _context.Managments.FindAsync(id);
            if (existing != null)
            {
                // Optionally: copy updated properties from parameter to existing
                _context.Entry(existing).CurrentValues.SetValues(managment);
            }
        }

        // Deletes a Managment entity by ID
        public async Task DeleteAsyncById(int id)
        {
            var managment = await _context.Managments.FindAsync(id);
            if (managment != null)
            {
                _context.Managments.Remove(managment);
            }
        }
    }
}

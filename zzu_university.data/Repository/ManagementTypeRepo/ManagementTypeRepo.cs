using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Managment;

namespace zzu_university.data.Repository.ManagementTypeRepo
{
    public class ManagementTypeRepo : IManagementTypeRepo
    {
        private readonly ApplicationDbContext _context;

        public ManagementTypeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ManagementType>> GetAllAsync()
        {
            return await _context.ManagementTypes.ToListAsync();
        }

        public async Task<ManagementType?> GetByIdAsync(int id)
        {
            return await _context.ManagementTypes.FindAsync(id);
        }

        public async Task AddAsync(ManagementType type)
        {
            await _context.ManagementTypes.AddAsync(type);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ManagementType updatedType)
        {
            var existing = await _context.ManagementTypes.FindAsync(id);
            if (existing != null)
            {
                existing.Name = updatedType.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var type = await _context.ManagementTypes.FindAsync(id);
            if (type != null)
            {
                _context.ManagementTypes.Remove(type);
                await _context.SaveChangesAsync();
            }
        }
    }
}

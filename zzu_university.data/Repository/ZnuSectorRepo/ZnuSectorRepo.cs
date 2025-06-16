using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Sector;

namespace zzu_university.data.Repository.ZnuSectorRepo
{
    public class ZnuSectorRepo : IZnuSectorRepo
    {
        private readonly ApplicationDbContext _context;

        public ZnuSectorRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ZnuSector>> GetAllAsync()
        {
            return await _context.ZnuSectors.Include(s => s.Departments).ToListAsync();
        }

        public async Task<ZnuSector?> GetByIdAsync(int id)
        {
            return await _context.ZnuSectors.Include(s => s.Departments)
                                            .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(ZnuSector sector)
        {
            await _context.ZnuSectors.AddAsync(sector);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}

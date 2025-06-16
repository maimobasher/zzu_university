using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Sector;

namespace zzu_university.data.Repository.ZnuSectorDetailsRepo
{
    public class ZnuSectorDetailRepo : IZnuSectorDetailRepo
    {
        private readonly ApplicationDbContext _context;

        public ZnuSectorDetailRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ZnuSectorDetail>> GetAllAsync()
        {
            return await _context.ZnuSectorDetails.ToListAsync();
        }

        public async Task<ZnuSectorDetail?> GetByIdAsync(int id)
        {
            return await _context.ZnuSectorDetails.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(ZnuSectorDetail detail)
        {
            await _context.ZnuSectorDetails.AddAsync(detail);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}

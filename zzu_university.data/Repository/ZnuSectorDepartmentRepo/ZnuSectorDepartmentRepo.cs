using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Sector;

namespace zzu_university.data.Repository.ZnuSectorDepartmentRepo
{
    public class ZnuSectorDepartmentRepo : IZnuSectorDepartmentRepo
    {
        private readonly ApplicationDbContext _context;

        public ZnuSectorDepartmentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ZnuSectorDepartment>> GetAllAsync()
        {
            return await _context.ZnuSectorDepartments.Include(d => d.Details).ToListAsync();
        }

        public async Task<ZnuSectorDepartment?> GetByIdAsync(int id)
        {
            return await _context.ZnuSectorDepartments.Include(d => d.Details)
                                                      .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(ZnuSectorDepartment department)
        {
            await _context.ZnuSectorDepartments.AddAsync(department);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}

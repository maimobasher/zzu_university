using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Faculty;

namespace zzu_university.data.Repository
{
    public class FacultyRepository : IFacultyRepo
    {
        private readonly ApplicationDbContext _context;

        public FacultyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Faculty>> GetAllAsync()
        {
            return await _context.Faculties.Include(f => f.AcadmicPrograms).ToListAsync();
        }

        public async Task<Faculty> GetByIdAsync(int id)
        {
            return await _context.Faculties.Include(f => f.AcadmicPrograms)
                                           .FirstOrDefaultAsync(f => f.FacultyId == id);
        }

        public async Task<Faculty> AddAsync(Faculty faculty)
        {
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();
            return faculty;
        }

        public async Task<Faculty> UpdateAsync(Faculty faculty)
        {
            _context.Faculties.Update(faculty);
            await _context.SaveChangesAsync();
            return faculty;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
                return false;

            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

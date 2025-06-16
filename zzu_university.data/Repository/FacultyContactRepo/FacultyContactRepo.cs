using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.FacultyContact;

namespace zzu_university.data.Repository.FacultyContactRepo
{
    public class FacultyContactRepo : IFacultyContactRepo
    {
        private readonly ApplicationDbContext _context;

        public FacultyContactRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FacultyContact>> GetAllAsync()
        {
            return await _context.FacultyContacts
                .Include(c => c.Faculty)
                .Include(c => c.Program)
                .ToListAsync();
        }

        public async Task<FacultyContact> GetByIdAsync(int id)
        {
            return await _context.FacultyContacts
                .Include(c => c.Faculty)
                .Include(c => c.Program)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(FacultyContact contact)
        {
            await _context.FacultyContacts.AddAsync(contact);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _context.FacultyContacts.FindAsync(id);
            if (contact == null)
                return false;

            _context.FacultyContacts.Remove(contact);
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

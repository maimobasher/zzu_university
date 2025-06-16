using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Complaints;

namespace zzu_university.data.Repository.ComplaintsRepo
{
    public class ComplaintRepository : IComplaintRepo
    {
        private readonly ApplicationDbContext _context;

        public ComplaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Complaint>> GetAllAsync()
        {
            return await _context.Complaints
                .Include(c => c.Student)
                .ToListAsync();
        }

        public async Task<Complaint> GetByIdAsync(int id)
        {
            return await _context.Complaints
                .Include(c => c.Student)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Complaint>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Complaints
                .Where(c => c.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<Complaint> AddAsync(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task<Complaint> UpdateAsync(Complaint complaint)
        {
            _context.Complaints.Update(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
                return false;

            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

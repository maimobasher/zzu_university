using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Certificate;

namespace zzu_university.data.Repository.CertificateRepo
{
    public class CertificateRepo: ICertificateRepo
    {
        private readonly ApplicationDbContext _context;

        public CertificateRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Certificate>> GetAllAsync()
        {
            return await _context.Certificates
                .Include(c => c.Student)
                .ToListAsync();
        }

        public async Task<Certificate> GetByIdAsync(int id)
        {
            return await _context.Certificates
                .Include(c => c.Student)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Certificate>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Certificates
                .Where(c => c.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<Certificate> AddAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }

        public async Task<Certificate> UpdateAsync(Certificate certificate)
        {
            _context.Certificates.Update(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null) return false;

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

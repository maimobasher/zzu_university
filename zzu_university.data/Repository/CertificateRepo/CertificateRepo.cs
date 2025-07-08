using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using zzu_university.data.Data;
using zzu_university.data.Model.Certificate;

namespace zzu_university.data.Repository.CertificateRepo
{
    public class CertificateRepo : ICertificateRepo
    {
        private readonly ApplicationDbContext _context;

        public CertificateRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Certificate> AddAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }

        public async Task<bool> DeleteAsync(int certificateId)
        {
            var cert = await _context.Certificates.FindAsync(certificateId);
            if (cert == null)
                return false;

            _context.Certificates.Remove(cert);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Certificate>> GetAllAsync()
        {
            return await _context.Certificates.ToListAsync(); // Removed Include
        }

        public async Task<Certificate> GetByIdAsync(int certificateId)
        {
            return await _context.Certificates
                .FirstOrDefaultAsync(c => c.Id == certificateId); // Removed Include
        }

        //public async Task<IEnumerable<Certificate>> GetByStudentIdAsync(int studentId)
        //{
        //    return await _context.Certificates
        //        .Where(c => c.StudentId == studentId)
        //        .ToListAsync();
        //}

        public async Task<Certificate> UpdateAsync(Certificate certificate)
        {
            var existing = await _context.Certificates.FindAsync(certificate.Id);
            if (existing == null)
                return null;

            existing.CertificateName = certificate.CertificateName;
            existing.IssueDate = certificate.IssueDate;
            existing.Description = certificate.Description;
            existing.is_deleted = certificate.is_deleted;
            //existing.StudentId = certificate.StudentId;

            _context.Certificates.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}

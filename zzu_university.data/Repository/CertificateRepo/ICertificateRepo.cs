using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Certificate;

namespace zzu_university.data.Repository.CertificateRepo
{
    public interface ICertificateRepo
    {
        Task<IEnumerable<Certificate>> GetAllAsync();
        Task<Certificate> GetByIdAsync(int id);
        Task<IEnumerable<Certificate>> GetByStudentIdAsync(int studentId);
        Task<Certificate> AddAsync(Certificate certificate);
        Task<Certificate> UpdateAsync(Certificate certificate);
        Task<bool> DeleteAsync(int id);
    }
}

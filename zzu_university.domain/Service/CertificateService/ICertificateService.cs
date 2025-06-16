using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.CertificateDto;

namespace zzu_university.domain.Service.CertificateService
{
    public interface ICertificateService
    {
        Task<IEnumerable<CertificateReadDto>> GetAllAsync();
        Task<CertificateReadDto> GetByIdAsync(int id);
        Task<IEnumerable<CertificateReadDto>> GetByStudentIdAsync(int studentId);
        Task<CertificateReadDto> CreateAsync(CertificateCreateDto dto);
        Task<CertificateReadDto> UpdateAsync(CertificateUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

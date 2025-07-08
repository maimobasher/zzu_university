using System.Collections.Generic;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.CertificateDto;

namespace zzu_university.domain.Service.CertificateService
{
    public interface ICertificateService
    {
        Task<IEnumerable<CertificateReadDto>> GetAllAsync(bool includeDeleted = false, bool onlyDeleted = false);
        Task<CertificateReadDto> GetByIdAsync(int id);
        Task<CertificateReadDto> CreateAsync(CertificateCreateDto dto);
        Task<CertificateReadDto> UpdateAsync(CertificateUpdateDto dto);
        Task<bool> DeleteAsync(int id); // حذف نهائي (إن أردت الإبقاء عليه)

        // ✅ Soft Delete = تعديل العمود IsDeleted إلى true
        Task<bool> SoftDeleteAsync(int id);

        // ✅ Restore = تعديل IsDeleted إلى false
        Task<bool> RestoreAsync(int id);

        // ✅ حذف نهائي من قاعدة البيانات
        Task<bool> HardDeleteAsync(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using zzu_university.data.Model.Certificate;

namespace zzu_university.data.Repository.CertificateRepo
{
    public interface ICertificateRepo
    {
        // الحصول على كل الشهادات
        Task<IEnumerable<Certificate>> GetAllAsync();

        // الحصول على شهادة حسب معرف الشهادة
        Task<Certificate> GetByIdAsync(int certificateId);

        // الحصول على كل الشهادات لطالب معين
        Task<IEnumerable<Certificate>> GetByStudentIdAsync(int studentId);

        // إضافة شهادة جديدة
        Task<Certificate> AddAsync(Certificate certificate);

        // تحديث شهادة
        Task<Certificate> UpdateAsync(Certificate certificate);

        // حذف شهادة
        Task<bool> DeleteAsync(int certificateId);
    }
}

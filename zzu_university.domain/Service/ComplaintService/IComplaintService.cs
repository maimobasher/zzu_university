using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.ComplaintsDto;

namespace zzu_university.domain.Service.ComplaintService
{
    public interface IComplaintService
    {
        Task<IEnumerable<ComplaintReadDto>> GetAllAsync();
        Task<ComplaintReadDto> GetByIdAsync(int id);
        Task<IEnumerable<ComplaintReadDto>> GetByStudentIdAsync(int studentId);
        Task<ComplaintReadDto> CreateAsync(ComplaintCreateDto dto, int? studentId);

        Task<bool> DeleteAsync(int id);
    }

}

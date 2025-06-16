using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.FacultyContact;

namespace zzu_university.domain.Service.FacultyContactService
{
    public interface IFacultyContactService
    {
        Task<IEnumerable<FacultyContactReadDto>> GetAllAsync();
        Task<FacultyContactReadDto> GetByIdAsync(int id);
        Task<FacultyContactReadDto> CreateAsync(FacultyContactCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

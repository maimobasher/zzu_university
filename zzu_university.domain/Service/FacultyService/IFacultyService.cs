using System.Collections.Generic;
using System.Threading.Tasks;
using zzu_university.data.DTOs;

namespace zzu_university.Services
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDto>> GetAllAsync();
        Task<FacultyDto> GetByIdAsync(int id);
        Task<FacultyDto> CreateAsync(FacultyDto dto);
        Task<FacultyDto> UpdateAsync(int id, FacultyDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

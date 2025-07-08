using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.ProgramDto;

namespace zzu_university.domain.Service.ProgramService
{
    public interface IProgramService
    {
        Task<IEnumerable<ProgramReadDto>> GetAllProgramsAsync();
        Task<ProgramReadDto> GetProgramByIdAsync(int id);
        Task CreateProgramAsync(ProgramCreateDto programCreateDto);
        Task UpdateProgramAsync(ProgramUpdateDto programUpdateDto);
        Task DeleteProgramAsync(int id);
        Task<IEnumerable<ProgramReadDto>> GetProgramsByFacultyIdAsync(int facultyId);

        Task<string> SoftDeleteProgramAsync(int id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.ProgramDetails;

namespace zzu_university.domain.Service.ProgramDetailsService
{
    public interface IProgramDetailsService
    {
        Task<IEnumerable<ProgramDetailsReadDto>> GetAllAsync();
        Task<ProgramDetailsReadDto> GetByIdAsync(int id);
        Task<ProgramDetailsReadDto> GetByProgramIdAsync(int programId);
        Task<ProgramDetailsReadDto> CreateAsync(ProgramDetailsCreateDto dto);
        Task<ProgramDetailsReadDto> UpdateAsync(int id, ProgramDetailsCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

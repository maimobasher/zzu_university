using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Program;

namespace zzu_university.data.Repository.ProgramRepo
{
    public interface IProgramRepo
    {
        Task<IEnumerable<AcadmicProgram>> GetAllProgramsAsync();
        Task<AcadmicProgram> GetProgramByIdAsync(int id);
        Task AddProgramAsync(AcadmicProgram program);
        Task UpdateProgramAsync(AcadmicProgram program);
        Task DeleteProgramAsync(int id);
    }
}

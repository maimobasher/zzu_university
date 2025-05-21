using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace zzu_university.data.Repository.ProgramRepo
{
    public interface IProgramRepo
    {
        Task<IEnumerable<AcadmicProgram>> GetAllProgramsAsync();
        Task<AcadmicProgram> GetProgramByIdAsync(int id);
        Task AddProgramAsync(AcadmicProgram program);
        Task UpdateProgramAsync(AcadmicProgram program);
        Task DeleteProgramAsync(int id);

        // التعديل هنا: استخدم النوع AcadmicProgram بدلاً من object
        Task<IEnumerable<AcadmicProgram>> FindAsync(Func<AcadmicProgram, bool> predicate);
    }
}

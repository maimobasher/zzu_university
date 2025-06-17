using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.ProgramDetails.zzu_university.data.Model.Program;

namespace zzu_university.data.Repository.ProgramDetailsRepo
{
    public interface IProgramDetailsRepo
    {
        Task<IEnumerable<ProgramDetails>> GetAllAsync();
        Task<ProgramDetails> GetByIdAsync(int id);
        Task<ProgramDetails> GetByProgramIdAsync(int programId);
        Task<ProgramDetails> AddAsync(ProgramDetails programDetails);
        Task<ProgramDetails> UpdateAsync(ProgramDetails programDetails);
        Task<bool> DeleteAsync(int id);
    }
}

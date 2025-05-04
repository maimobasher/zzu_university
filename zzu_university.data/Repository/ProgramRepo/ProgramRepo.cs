using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Program;

namespace zzu_university.data.Repository.ProgramRepo
{
    public class ProgramRepo:IProgramRepo
    {
        private readonly ApplicationDbContext _context;

        public ProgramRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AcadmicProgram>> GetAllProgramsAsync()
        {
            return await _context.Programs.ToListAsync();
        }

        public async Task<AcadmicProgram> GetProgramByIdAsync(int id)
        {
            return await _context.Programs.FindAsync(id);
        }

        public async Task AddProgramAsync(AcadmicProgram program)
        {
            await _context.Programs.AddAsync(program);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProgramAsync(AcadmicProgram program)
        {
            _context.Programs.Update(program);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProgramAsync(int id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program != null)
            {
                _context.Programs.Remove(program);
                await _context.SaveChangesAsync();
            }
        }
    }
}


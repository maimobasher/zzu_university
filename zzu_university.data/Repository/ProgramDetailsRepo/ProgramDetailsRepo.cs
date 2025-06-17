using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.ProgramDetails.zzu_university.data.Model.Program;

namespace zzu_university.data.Repository.ProgramDetailsRepo
{
    public class ProgramDetailsRepo : IProgramDetailsRepo
    {
        private readonly ApplicationDbContext _context;

        public ProgramDetailsRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProgramDetails>> GetAllAsync()
        {
            return await _context.ProgramDetails
                .Include(p => p.Program)
                .ToListAsync();
        }

        public async Task<ProgramDetails> GetByIdAsync(int id)
        {
            return await _context.ProgramDetails
                .Include(p => p.Program)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProgramDetails> GetByProgramIdAsync(int programId)
        {
            return await _context.ProgramDetails
                .Include(p => p.Program)
                .FirstOrDefaultAsync(p => p.ProgramId == programId);
        }

        public async Task<ProgramDetails> AddAsync(ProgramDetails programDetails)
        {
            _context.ProgramDetails.Add(programDetails);
            await _context.SaveChangesAsync();
            return programDetails;
        }

        public async Task<ProgramDetails> UpdateAsync(ProgramDetails programDetails)
        {
            _context.ProgramDetails.Update(programDetails);
            await _context.SaveChangesAsync();
            return programDetails;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.ProgramDetails.FindAsync(id);
            if (existing == null)
                return false;

            _context.ProgramDetails.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

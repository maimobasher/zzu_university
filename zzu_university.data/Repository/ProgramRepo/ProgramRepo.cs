using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;

namespace zzu_university.data.Repository.ProgramRepo
{
    public class ProgramRepo : IProgramRepo
    {
        private readonly ApplicationDbContext _context;

        public ProgramRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> GetFacultyNameByProgramIdAsync(int programId)
        {
            return await _context.Programs
                .Where(p => p.ProgramId == programId)
                .Include(p => p.Faculty)
                .Select(p => p.Faculty.Name)
                .FirstOrDefaultAsync();
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
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(s => s.StudentId == id);
        }
        // هذا التنفيذ يستدعي تحميل كل البرامج للذاكرة ثم يطبق الفلتر
        // انتبه: هذا قد يكون غير فعال إذا عدد البرامج كبير جداً
        public async Task<IEnumerable<AcadmicProgram>> FindAsync(Func<AcadmicProgram, bool> predicate)
        {
            var allPrograms = await _context.Programs.ToListAsync(); // جلب كل البرامج
            return allPrograms.Where(predicate); // تطبيق الفلتر في الذاكرة
        }
    }
}

using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.Payment;

namespace zzu_university.data.Repository.StudentRepo
{
    public class StudentRepo:IStudentRepo
    {
        private readonly ApplicationDbContext _context;

        public StudentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(s => s.StudentId == id);
        }
        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Student> GetStudentWithProgramAndRegistrationsAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Program)
                .Include(s => s.ProgramRegistrations)
                    .ThenInclude(pr => pr.Program)
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }
        public async Task<Student> GetStudentWithSpecificProgramAsync(int studentId, int programId)
        {
            return await _context.Students
                .Include(s => s.ProgramRegistrations.Where(r => r.ProgramId == programId))
                    .ThenInclude(r => r.Program)
                        .ThenInclude(p => p.Faculty) // ✅ تحميل الكلية
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }


        public async Task<StudentPayment> GetPaymentAsync(int studentId, int programId)
        {
            return await _context.StudentPayments
                .Where(p => p.StudentId == studentId && p.ProgramId == programId)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefaultAsync();
        }

    }
}


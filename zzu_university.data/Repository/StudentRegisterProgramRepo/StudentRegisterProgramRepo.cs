using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.StudentRegisterProgram;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

public class StudentRegisterProgramRepository : IStudentRegisterProgramRepo
{
    private readonly ApplicationDbContext _context;

    public StudentRegisterProgramRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentRegisterProgram>> GetAllAsync()
    {
        return await _context.StudentRegisterPrograms
            .Include(srp => srp.Program)
            .Include(srp => srp.Student)
            .Where(srp => srp.Program != null && srp.RegistrationCode != null) // ✅ تجنب الصفوف اللي فيها null
            .ToListAsync();
    }


    public async Task<bool> AnyAsync(Expression<Func<StudentRegisterProgram, bool>> predicate)
    {
        return await _context.StudentRegisterPrograms.AnyAsync(predicate);
    }


    public async Task<StudentRegisterProgram> GetByIdAsync(int id)
    {
        return await _context.StudentRegisterPrograms
            .Include(srp => srp.Student)
            .Include(srp => srp.Program)
            .FirstOrDefaultAsync(srp => srp.Id == id);
    }

    public async Task AddAsync(StudentRegisterProgram entity)
    {
        await _context.StudentRegisterPrograms.AddAsync(entity);
    }

    public void Update(StudentRegisterProgram entity)
    {
        _context.StudentRegisterPrograms.Update(entity);
    }

    public void Delete(StudentRegisterProgram entity)
    {
        _context.StudentRegisterPrograms.Remove(entity);
    }

    // تمت إزالة SaveChangesAsync من هنا، حيث سيقوم الـ UnitOfWork بهذا
}

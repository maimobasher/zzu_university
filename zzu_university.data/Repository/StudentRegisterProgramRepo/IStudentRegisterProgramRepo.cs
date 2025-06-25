using System.Linq.Expressions;
using zzu_university.data.Model.StudentRegisterProgram;

public interface IStudentRegisterProgramRepo
{
    Task<IEnumerable<StudentRegisterProgram>> GetAllAsync();
    Task<StudentRegisterProgram> GetByIdAsync(int id);
    Task AddAsync(StudentRegisterProgram entity);
    void Update(StudentRegisterProgram entity);
    void Delete(StudentRegisterProgram entity);
    Task<bool> AnyAsync(Expression<Func<StudentRegisterProgram, bool>> predicate);
    //Task<bool> SaveChangesAsync();
}

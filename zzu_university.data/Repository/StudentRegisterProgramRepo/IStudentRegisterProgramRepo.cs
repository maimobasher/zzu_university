using zzu_university.data.Model.StudentRegisterProgram;

public interface IStudentRegisterProgramRepo
{
    Task<IEnumerable<StudentRegisterProgram>> GetAllAsync();
    Task<StudentRegisterProgram> GetByIdAsync(int id);
    Task AddAsync(StudentRegisterProgram entity);
    void Update(StudentRegisterProgram entity);
    void Delete(StudentRegisterProgram entity);
    //Task<bool> SaveChangesAsync();
}

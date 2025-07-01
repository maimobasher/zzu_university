using zzu_university.data.Model.Payment;
using zzu_university.data.Model.StudentRegisterProgram;

namespace zzu_university.data.Repository.StudentRepo
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Student> GetStudentWithProgramAndRegistrationsAsync(int id);
        Task<Student> GetStudentWithSpecificProgramAsync(int studentId, int programId);
        Task<StudentPayment> GetPaymentAsync(int studentId, int programId);
        Task<StudentRegisterProgram> GetStudentRegistrationWithFacultyAsync(int studentId, int programId);

        Task<StudentRegisterProgram> GetStudentRegistrationWithProgramAndFacultyAsync(int studentId, int programId);

    }
}

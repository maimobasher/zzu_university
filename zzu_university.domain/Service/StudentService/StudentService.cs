using zzu_university.data.Model.Student;
using zzu_university.domain.StudentDto;

namespace zzu_university.domain.Service.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync()
        {
            var students = await _unitOfWork.Student.GetAllStudentsAsync();
            return students.Select(s => new StudentReadDto
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                NationalId = s.NationalId,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email,
                ProgramName = s.Program?.Name,
                IsPaymentCompleted = s.IsPaymentCompleted
            });
        }

        public async Task<StudentReadDto> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Student.GetStudentByIdAsync(id);
            if (student == null) return null;

            return new StudentReadDto
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                NationalId = student.NationalId,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                ProgramName = student.Program?.Name,
                IsPaymentCompleted = student.IsPaymentCompleted
            };
        }

        public async Task<StudentReadDto> CreateStudentAsync(StudentCreateDto studentCreateDto)
        {
            var student = new Student
            {
                FullName = studentCreateDto.FullName,
                NationalId = studentCreateDto.NationalId,
                PhoneNumber = studentCreateDto.PhoneNumber,
                Email = studentCreateDto.Email,
                SelectedProgramId = studentCreateDto.SelectedProgramId,
                IsPaymentCompleted = false
            };

            await _unitOfWork.Student.AddStudentAsync(student);
            await _unitOfWork.SaveAsync();

            return new StudentReadDto
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                NationalId = student.NationalId,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                ProgramName = "", // تقدر تجيب اسم البرنامج لو احتجت
                IsPaymentCompleted = student.IsPaymentCompleted
            };
        }

        public async Task<StudentReadDto> UpdateStudentAsync(StudentUpdateDto studentUpdateDto)
        {
            var student = await _unitOfWork.Student.GetStudentByIdAsync(studentUpdateDto.StudentId);
            if (student == null) return null;

            student.FullName = studentUpdateDto.FullName;
            student.NationalId = studentUpdateDto.NationalId;
            student.PhoneNumber = studentUpdateDto.PhoneNumber;
            student.Email = studentUpdateDto.Email;
            student.SelectedProgramId = studentUpdateDto.SelectedProgramId;
            student.IsPaymentCompleted = studentUpdateDto.IsPaymentCompleted;

            await _unitOfWork.Student.UpdateStudentAsync(student);
            await _unitOfWork.SaveAsync();

            return new StudentReadDto
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                NationalId = student.NationalId,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                ProgramName = "", // تقدر تجيب اسم البرنامج لو احتجت
                IsPaymentCompleted = student.IsPaymentCompleted
            };
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.Student.GetStudentByIdAsync(id);
            if (student == null) return false;

            await _unitOfWork.Student.DeleteStudentAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}

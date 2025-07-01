using zzu_university.data.Model.Certificate;
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
                firstName = s.firstName,
                lastName = s.lastName,
                middleName = s.middleName,
                nationalId = s.nationalId,
                nationality = s.nationality,
                phone = s.phone,
                dateOfBirth = s.dateOfBirth,
                address = s.address,
                email = s.email,
                gender = s.gender,
                city = s.city,
                //postalCode = s.postalCode,
                LiscenceType = s.LiscenceType,
                guardianPhone = s.guardianPhone ,
                gpa = s.gpa,
                // highSchool = s.highSchool,
                percent = s.percent,
                graduationYear = s.graduationYear,
                faculty = s.faculty,
                //semester = s.semester,
                program = s.Program?.Name,
                IsPaymentCompleted = s.IsPaymentCompleted,
                UserName = s.UserName,
                Password = s.Password,
                doc_url = s.doc_url
                //CertificateId = s.Certificate?.CertificateId ?? 0


            });
        }

        public async Task<StudentReadDto> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Student.GetStudentByIdAsync(id);
            if (student == null) return null;

            return new StudentReadDto
            {
                StudentId = student.StudentId,
                firstName = student.firstName,
                lastName = student.lastName,
                middleName = student.middleName,
                nationality = student.nationality,
                dateOfBirth = student.dateOfBirth,
                address = student.address,
                city = student.city,
                nationalId = student.nationalId,
                graduationYear = student.graduationYear,
                gender= student.gender,
                gpa = student.gpa,
               // highSchool = student.highSchool,
               percent = student.percent,
                faculty = student.faculty,
                //semester = student.semester,
                guardianPhone = student.guardianPhone,
                LiscenceType = student.LiscenceType,
                phone = student.phone,
                email = student.email,
                //postalCode = student.postalCode,
                program = student.Program?.Name,
                IsPaymentCompleted = student.IsPaymentCompleted,
                UserName = student.UserName,
                Password = student.Password,
                doc_url = student.doc_url
            };
        }

        public async Task<StudentReadDto> CreateStudentAsync(StudentCreateDto studentCreateDto)
        {
            var student = new Student
            {
                firstName= studentCreateDto.firstName,
                middleName = studentCreateDto.middleName,
                lastName = studentCreateDto.lastName,
                nationality= studentCreateDto.nationality,
                dateOfBirth = studentCreateDto.dateOfBirth,
                address = studentCreateDto.address,
                city = studentCreateDto.city,
                //postalCode = studentCreateDto.postalCode,
                LiscenceType = studentCreateDto.LiscenceType,
                guardianPhone = studentCreateDto.guardianPhone,
                program = studentCreateDto.program,
                gender = studentCreateDto.gender,
                graduationYear = studentCreateDto.graduationYear,
                gpa = studentCreateDto.gpa,
                // highSchool = studentCreateDto.highSchool,
                percent = studentCreateDto.percent,
                faculty = studentCreateDto.faculty,
               // semester = studentCreateDto.semester,
                nationalId = studentCreateDto.nationalId,
                phone = studentCreateDto.phone,
                email = studentCreateDto.email,
                SelectedProgramId = studentCreateDto.SelectedProgramId,
                IsPaymentCompleted = false,
                UserName = studentCreateDto.UserName,
                Password = studentCreateDto.Password,
                CertificateId = studentCreateDto.CertificateId,
                doc_url = studentCreateDto.doc_url // URL to the student's document (e.g., national ID, passport, etc.)
            };

            await _unitOfWork.Student.AddStudentAsync(student);
            await _unitOfWork.SaveAsync();

            return new StudentReadDto
            {
                StudentId = student.StudentId,
                firstName = student.firstName,
                lastName = student.lastName,
                middleName = student.middleName,
                nationality = student.nationality,
                dateOfBirth = student.dateOfBirth,
                address = student.address,
                city = student.city,
                // postalCode = student.postalCode,
                LiscenceType = student.LiscenceType,
                guardianPhone = student.guardianPhone,
                gender =student.gender,
                gpa = student.gpa,
                // highSchool = student.highSchool,
                percent = student.percent,
                graduationYear = student.graduationYear,
                faculty = student.faculty,
               // semester = student.semester,
                nationalId = student.nationalId,
                phone = student.phone,
                email = student.email,
                program = "", // تقدر تجيب اسم البرنامج لو احتجت
                IsPaymentCompleted = student.IsPaymentCompleted,
                UserName=student.UserName,  
                Password = student.Password,
                doc_url = student.doc_url
            };
        }

        public async Task<StudentReadDto> UpdateStudentAsync(StudentUpdateDto studentUpdateDto)
        {
            var student = await _unitOfWork.Student.GetStudentByIdAsync(studentUpdateDto.StudentId);
            if (student == null) return null;

            student.firstName = studentUpdateDto.firstName;
            student.middleName = studentUpdateDto.middleName;
            student.lastName = studentUpdateDto.lastName;
            student.nationality = studentUpdateDto.nationality;
            student.nationalId = studentUpdateDto.nationalId;
            student.phone = studentUpdateDto.phone;
            student.dateOfBirth = studentUpdateDto.dateOfBirth;
            student.address = studentUpdateDto.address;
            student.city = studentUpdateDto.city;
            // student.postalCode = studentUpdateDto.postalCode;
            student.LiscenceType = studentUpdateDto.LiscenceType;
            student.guardianPhone = studentUpdateDto.guardianPhone;
            //student.highSchool = studentUpdateDto.highSchool;
            student.percent = studentUpdateDto.percent;
            student.graduationYear = studentUpdateDto.graduationYear;
            student.gpa = studentUpdateDto.gpa;
            student.faculty = studentUpdateDto.faculty;
            student.gender = studentUpdateDto.gender;
            //student.semester = studentUpdateDto.semester;
            student.program = studentUpdateDto.program;
            student.email = studentUpdateDto.email;
            student.SelectedProgramId = studentUpdateDto.SelectedProgramId;
            student.IsPaymentCompleted = studentUpdateDto.IsPaymentCompleted;
            student.UserName = studentUpdateDto.UserName;
            student.Password = studentUpdateDto.Password;
            student.doc_url = studentUpdateDto.doc_url; // URL to the student's document (e.g., national ID, passport, etc.)
            await _unitOfWork.Student.UpdateStudentAsync(student);
            await _unitOfWork.SaveAsync();

            return new StudentReadDto
            {
                StudentId = student.StudentId,
                firstName = student.firstName,
                lastName = student.lastName,
                middleName = student.middleName,
                nationality = student.nationality,
                nationalId = student.nationalId,
                phone = student.phone,
                dateOfBirth = student.dateOfBirth,
                address = student.address,
                city = student.city,
                // postalCode = student.postalCode,
                LiscenceType = student.LiscenceType,
                guardianPhone = student.guardianPhone,
                gender = student.gender,
                gpa = student.gpa,
                //  highSchool = student.highSchool,
                percent = student.percent,
                graduationYear = student.graduationYear,
                faculty = student.faculty,
               // semester = student.semester,
                email = student.email,
                program= "", // تقدر تجيب اسم البرنامج لو احتجت
                IsPaymentCompleted = student.IsPaymentCompleted,
                UserName = student.UserName,
                Password = student.Password,
                doc_url = student.doc_url // URL to the student's document (e.g., national ID, passport, etc.)
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

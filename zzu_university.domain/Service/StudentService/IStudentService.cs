using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.StudentDto;

namespace zzu_university.domain.Service.StudentService
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync();
        Task<StudentReadDto> GetStudentByIdAsync(int id);
        Task<StudentReadDto> CreateStudentAsync(StudentCreateDto studentCreateDto);
        Task<StudentReadDto> UpdateStudentAsync(StudentUpdateDto studentUpdateDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using zzu_university.data.DTOs;
using zzu_university.domain.DTOS;

namespace zzu_university.data.Services
{
    public interface IStudentRegisterProgramService
    {
        Task<List<StudentRegisterProgramDto>> GetAllAsync();
        Task<StudentRegisterProgramDto> GetByIdAsync(int id);
        Task<StudentRegisterProgramDto> CreateAsync(StudentRegisterProgramDto dto);
        Task<bool> UpdateAsync(int id, StudentRegisterProgramDto dto);
        Task<bool> DeleteAsync(int id);
        Task<string> GenerateNextRegistrationCodeAsync(int programId);
    }
}

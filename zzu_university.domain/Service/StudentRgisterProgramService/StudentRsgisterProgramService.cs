using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.DTOs;
using zzu_university.data.Model.StudentRegisterProgram;
using zzu_university.domain.DTOS;

namespace zzu_university.data.Services
{
    public class StudentRegisterProgramService:IStudentRegisterProgramService
    {
        private readonly ApplicationDbContext _context;

        public StudentRegisterProgramService(ApplicationDbContext context)
        {
            _context = context;
        }

        // جلب كل التسجيلات
        public async Task<List<StudentRegisterProgramDto>> GetAllAsync()
        {
            return await _context.StudentRegisterPrograms
                .AsNoTracking()
                .Select(static srp => new StudentRegisterProgramDto
                {
                    Id = srp.Id,
                    StudentId = srp.StudentId,
                    ProgramId = srp.ProgramId,
                    RegistrationCode = srp.RegistrationCode,
                    RegisterDate = srp.RegisterDate,
                    ProgramCode = srp.Program != null ? srp.Program.ProgramCode : null,
                    status = srp.status




                })
                .ToListAsync();
        }

        // جلب تسجيل حسب Id
        public async Task<StudentRegisterProgramDto> GetByIdAsync(int id)
        {
            var srp = await _context.StudentRegisterPrograms
                .Include(srp => srp.Program)
                .FirstOrDefaultAsync(srp => srp.Id == id);

            if (srp == null) return null;

            return new StudentRegisterProgramDto
            {
                Id = srp.Id,
                StudentId = srp.StudentId,
                ProgramId = srp.ProgramId,
                RegistrationCode = srp.RegistrationCode,
                RegisterDate = srp.RegisterDate,
                ProgramCode = srp.Program != null ? srp.Program.ProgramCode : null,
                status = srp.status
            };
        }

        
        // إضافة تسجيل جديد
        public async Task<StudentRegisterProgramDto> CreateAsync(StudentRegisterProgramDto dto)
        {
            var entity = new StudentRegisterProgram
            {
                StudentId = dto.StudentId,
                ProgramId = dto.ProgramId,
                RegistrationCode = dto.RegistrationCode,
                RegisterDate = dto.RegisterDate,
                ProgramCode = dto.ProgramCode,
                status = dto.status
            };

            _context.StudentRegisterPrograms.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        // تحديث تسجيل موجود
        public async Task<bool> UpdateAsync(int id, StudentRegisterProgramDto dto)
        {
            var entity = await _context.StudentRegisterPrograms.FindAsync(id);
            if (entity == null) return false;

            entity.StudentId = dto.StudentId;
            entity.ProgramId = dto.ProgramId;
            entity.RegistrationCode = dto.RegistrationCode;
            entity.RegisterDate = dto.RegisterDate;
            entity.ProgramCode = dto.ProgramCode;
            entity.status = dto.status;

            await _context.SaveChangesAsync();
            return true;
        }

        // حذف تسجيل
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StudentRegisterPrograms.FindAsync(id);
            if (entity == null) return false;

            _context.StudentRegisterPrograms.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

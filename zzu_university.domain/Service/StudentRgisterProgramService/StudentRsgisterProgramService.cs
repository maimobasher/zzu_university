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
                .Include(srp => srp.Program)
                .AsNoTracking()
                .Select(srp => new StudentRegisterProgramDto
                {
                    Id = srp.Id,
                    StudentId = srp.StudentId,
                    ProgramId = srp.ProgramId,
                    RegistrationCode = srp.RegistrationCode,
                    RegisterDate = string.IsNullOrWhiteSpace(srp.RegisterDate)
                        ? DateTime.Now.ToString("yyyy-MM-dd")
                        : srp.RegisterDate,
                    ProgramCode = srp.Program != null ? srp.Program.ProgramCode : null,
                    status = srp.status,
                    ProgramAndReferenceCode = srp.ProgramAndReferenceCode
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
                status = srp.status,
                ProgramAndReferenceCode = srp.ProgramAndReferenceCode
            };
        }


        // إضافة تسجيل جديد
        public async Task<StudentRegisterProgramDto> CreateAsync(StudentRegisterProgramDto dto)
        {
            // تعيين الحالة الافتراضية إن لم تُرسل
            if (string.IsNullOrWhiteSpace(dto.status))
                dto.status = "pending";

            // التأكد من تنسيق التاريخ أو تعيين التاريخ الحالي
            if (string.IsNullOrWhiteSpace(dto.RegisterDate) || !DateTime.TryParse(dto.RegisterDate, out var parsedDate))
                parsedDate = DateTime.Now;

            // تنسيق التاريخ بشكل موحد قبل التخزين
            dto.RegisterDate = parsedDate.ToString("yyyy-MM-dd");

            var entity = new StudentRegisterProgram
            {
                StudentId = dto.StudentId,
                ProgramId = dto.ProgramId,
                RegistrationCode = dto.RegistrationCode,
                RegisterDate = dto.RegisterDate,
                ProgramCode = dto.ProgramCode,
                status = dto.status,
                ProgramAndReferenceCode = dto.ProgramAndReferenceCode
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
            entity.ProgramAndReferenceCode = dto.ProgramAndReferenceCode;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<string> GenerateNextRegistrationCodeAsync(int programId)
        {
            var validCodes = await _context.StudentRegisterPrograms
                .Where(x => x.ProgramId == programId && !string.IsNullOrWhiteSpace(x.RegistrationCode))
                .Select(x => x.RegistrationCode)
                .ToListAsync();

            var numericCodes = new List<int>();

            foreach (var code in validCodes)
            {
                if (!string.IsNullOrWhiteSpace(code) && int.TryParse(code, out var number))
                {
                    numericCodes.Add(number);
                }
            }

            var nextCode = numericCodes.Any() ? numericCodes.Max() + 1 : 1;
            return nextCode.ToString("D4");
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

using Microsoft.EntityFrameworkCore;
using zzu_university.domain.DTOS.ProgramDto;

namespace zzu_university.domain.Service.ProgramService
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProgramReadDto>> GetAllProgramsAsync()
        {
            var programs = await _unitOfWork.Program.GetAllProgramsAsync();

            // تحويل يدوي من Program إلى ProgramReadDto
            var programDtos = programs.Select(p => new ProgramReadDto
            {
                ProgramId = p.ProgramId,
                Name = p.Name,
                Description = p.Description,
                TuitionFees = p.TuitionFees,
                DurationInYears = p.DurationInYears,
                ProgramCode = p.ProgramCode,
                FacultyId = p.FacultyId,
                is_deleted=p.is_deleted
            });

            return programDtos;
        }
        public async Task<IEnumerable<ProgramReadDto>> GetProgramsByFacultyIdAsync(int facultyId)
        {
            var programs = await _unitOfWork.Program.FindAsync(p => p.FacultyId == facultyId);
            return programs.Select(p => new ProgramReadDto
            {
                ProgramId = p.ProgramId,
                Name = p.Name,
                Description = p.Description,
                TuitionFees = p.TuitionFees,
                DurationInYears = p.DurationInYears,
                FacultyId = p.FacultyId,
                ProgramCode = p.ProgramCode ,
                is_deleted = p.is_deleted   
            });
        }

       

        public async Task<ProgramReadDto> GetProgramByIdAsync(int id)
        {
            var program = await _unitOfWork.Program.GetProgramByIdAsync(id);

            // تحويل يدوي من Program إلى ProgramReadDto
            var programDto = new ProgramReadDto
            {
                ProgramId = program.ProgramId,
                Name = program.Name,
                Description = program.Description,
                TuitionFees = program.TuitionFees,
                DurationInYears = program.DurationInYears,
                ProgramCode = program.ProgramCode,
                FacultyId = program.FacultyId,
                is_deleted= program.is_deleted  
            };

            return programDto;
        }

        public async Task CreateProgramAsync(ProgramCreateDto programCreateDto)
        {
            // تحويل يدوي من ProgramCreateDto إلى AcadmicProgram
            var program = new AcadmicProgram
            {
                Name = programCreateDto.Name,
                Description = programCreateDto.Description,
                TuitionFees = programCreateDto.TuitionFees,
                DurationInYears = programCreateDto.DurationInYears,
                ProgramCode = programCreateDto.ProgramCode,
                FacultyId = programCreateDto.FacultyId,
                is_deleted = programCreateDto.is_deleted    
            };

            await _unitOfWork.Program.AddProgramAsync(program);
            await _unitOfWork.CompleteAsync(); // حفظ التغييرات باستخدام CompleteAsync
        }

        public async Task UpdateProgramAsync(ProgramUpdateDto programUpdateDto)
        {
            // تحويل يدوي من ProgramUpdateDto إلى AcadmicProgram
            var program = new AcadmicProgram
            {
                ProgramId = programUpdateDto.ProgramId,
                Name = programUpdateDto.Name,
                Description = programUpdateDto.Description,
                TuitionFees = programUpdateDto.TuitionFees,
                DurationInYears = programUpdateDto.DurationInYears,
                ProgramCode = programUpdateDto.ProgramCode,
                FacultyId = programUpdateDto.FacultyId,
                is_deleted = programUpdateDto.is_deleted    

            };

            await _unitOfWork.Program.UpdateProgramAsync(program);
            await _unitOfWork.CompleteAsync(); // حفظ التغييرات باستخدام CompleteAsync
        }
        public async Task<string> SoftDeleteProgramAsync(int id)
        {
            var program = await _unitOfWork.Program.GetProgramByIdAsync(id);

            if (program == null)
                return "not_found";

            if (program.is_deleted)
            {
                // ✅ البرنامج محذوف مسبقًا → أضيفي نسخة جديدة بنفس البيانات
                var newProgram = new AcadmicProgram
                {
                    Name = program.Name,
                    Description = program.Description,
                    TuitionFees = program.TuitionFees,
                    DurationInYears = program.DurationInYears,
                    ProgramCode = program.ProgramCode + "_COPY", // إضافة شيء بسيط لتمييز النسخة
                    FacultyId = program.FacultyId,
                    is_deleted = false
                };

                await _unitOfWork.Program.AddProgramAsync(newProgram);
                await _unitOfWork.CompleteAsync();
                return "restored";
            }

            // ✅ Soft Delete
            program.is_deleted = true;
            await _unitOfWork.Program.UpdateProgramAsync(program);
            await _unitOfWork.CompleteAsync();
            return "deleted";
        }

        public async Task DeleteProgramAsync(int id)
        {
            await _unitOfWork.Program.DeleteProgramAsync(id);
            await _unitOfWork.CompleteAsync(); // حفظ التغييرات باستخدام CompleteAsync
        }
    }
}

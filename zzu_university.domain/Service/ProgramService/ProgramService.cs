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
                DurationInYears = p.DurationInYears
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
                ProgramCode = p.ProgramCode 
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
                DurationInYears = program.DurationInYears
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
                DurationInYears = programCreateDto.DurationInYears
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
                DurationInYears = programUpdateDto.DurationInYears
            };

            await _unitOfWork.Program.UpdateProgramAsync(program);
            await _unitOfWork.CompleteAsync(); // حفظ التغييرات باستخدام CompleteAsync
        }

        public async Task DeleteProgramAsync(int id)
        {
            await _unitOfWork.Program.DeleteProgramAsync(id);
            await _unitOfWork.CompleteAsync(); // حفظ التغييرات باستخدام CompleteAsync
        }
    }
}

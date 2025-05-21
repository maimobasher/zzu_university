using zzu_university.data.DTOs;
using zzu_university.data.Model.Faculty;

namespace zzu_university.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FacultyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllAsync()
        {
            var faculties = await _unitOfWork.Faculty.GetAllAsync();
            return faculties.Select(f => new FacultyDto
            {
                FacultyId = f.FacultyId,
                Name = f.Name
            });
        }

        public async Task<FacultyDto> GetByIdAsync(int id)
        {
            var faculty = await _unitOfWork.Faculty.GetByIdAsync(id);
            if (faculty == null) return null;

            return new FacultyDto
            {
                FacultyId = faculty.FacultyId,
                Name = faculty.Name
            };
        }

        public async Task<FacultyDto> CreateAsync(FacultyDto dto)
        {
            var faculty = new Faculty
            {
                Name = dto.Name
            };

            var created = await _unitOfWork.Faculty.AddAsync(faculty);
            await _unitOfWork.SaveAsync();

            return new FacultyDto
            {
                FacultyId = created.FacultyId,
                Name = created.Name
            };
        }

        public async Task<FacultyDto> UpdateAsync(int id, FacultyDto dto)
        {
            var existing = await _unitOfWork.Faculty.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            var updated = await _unitOfWork.Faculty.UpdateAsync(existing);
            await _unitOfWork.SaveAsync();

            return new FacultyDto
            {
                FacultyId = updated.FacultyId,
                Name = updated.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _unitOfWork.Faculty.DeleteAsync(id);
            if (result)
                await _unitOfWork.SaveAsync();

            return result;
        }
    }
}

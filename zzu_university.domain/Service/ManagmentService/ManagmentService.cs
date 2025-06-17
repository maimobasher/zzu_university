using zzu_university.data.Model;
using zzu_university.data.Repository.ManagmentRepo;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.ManagmentService
{
    public class ManagmentService : IManagmentService
    {
        private readonly IManagmentRepo _repo;

        public ManagmentService(IManagmentRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ManagmentDto>> GetAllManagmentsAsync()
        {
            var result = await _repo.GetAllWithTypeAsync();
            return result.Select(m => new ManagmentDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                ContactEmail = m.ContactEmail,
                PhoneNumber = m.PhoneNumber,
                Type = m.Type,
                ImageUrl = m.ImageUrl
            });
        }

        public async Task<ManagmentDto> GetManagmentAsync(int id)
        {
            var m = await _repo.GetWithTypeByIdAsync(id);
            if (m == null) return null;

            return new ManagmentDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                ContactEmail = m.ContactEmail,
                PhoneNumber = m.PhoneNumber,
                Type = m.Type,
                ImageUrl = m.ImageUrl
            };
        }

        public async Task<ManagmentDto> AddManagmentAsync(ManagmentDto dto)
        {
            var newM = new Management
            {
                Name = dto.Name,
                Description = dto.Description,
                ContactEmail = dto.ContactEmail,
                PhoneNumber = dto.PhoneNumber,
                Type = dto.Type,
                ImageUrl = dto.ImageUrl
            };

            await _repo.AddAsync(newM);

            dto.Id = newM.Id;
            return dto;
        }

        public async Task<bool> UpdateManagmentAsync(int id, ManagmentDto dto)
        {
            var existing = await _repo.GetWithTypeByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.ContactEmail = dto.ContactEmail;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.Type = dto.Type;
            existing.ImageUrl = dto.ImageUrl;

            await _repo.UpdateAsyncById(id, existing);
            return true;
        }

        public async Task<bool> DeleteManagmentAsync(int id)
        {
            var existing = await _repo.GetWithTypeByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsyncById(id);
            return true;
        }
    }
}

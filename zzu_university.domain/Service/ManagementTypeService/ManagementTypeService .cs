namespace zzu_university.domain.Service.ManagementTypeService
{
    using global::zzu_university.data.Model.Managment;
    using global::zzu_university.data.Repository.ManagementTypeRepo;
    using global::zzu_university.domain.DTOS.ManagementType.zzu_university.domain.DTOS;

    namespace zzu_university.domain.Service.ManagementTypeService
    {
        public class ManagementTypeService : IManagementTypeService
        {
            private readonly IManagementTypeRepo _repo;

            public ManagementTypeService(IManagementTypeRepo repo)
            {
                _repo = repo;
            }

            public async Task<IEnumerable<ManagementTypeDto>> GetAllAsync()
            {
                var list = await _repo.GetAllAsync();
                return list.Select(t => new ManagementTypeDto
                {
                    Id = t.Id,
                    Name = t.Name
                });
            }

            public async Task<ManagementTypeDto?> GetByIdAsync(int id)
            {
                var type = await _repo.GetByIdAsync(id);
                if (type == null) return null;

                return new ManagementTypeDto
                {
                    Id = type.Id,
                    Name = type.Name
                };
            }

            public async Task<ManagementTypeDto> AddAsync(ManagementTypeDto dto)
            {
                var newType = new ManagementType
                {
                    Name = dto.Name
                };

                await _repo.AddAsync(newType);
                dto.Id = newType.Id;
                return dto;
            }

            public async Task<bool> UpdateAsync(int id, ManagementTypeDto dto)
            {
                var existing = await _repo.GetByIdAsync(id);
                if (existing == null) return false;

                existing.Name = dto.Name;
                await _repo.UpdateAsync(id, existing);
                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var existing = await _repo.GetByIdAsync(id);
                if (existing == null) return false;

                await _repo.DeleteAsync(id);
                return true;
            }
        }
    }

}

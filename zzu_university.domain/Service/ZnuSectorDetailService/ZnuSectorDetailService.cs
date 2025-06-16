using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Sector;
using zzu_university.data.Repository.ZnuSectorDetailsRepo;
using zzu_university.domain.DTOS.SectorDto;

namespace zzu_university.domain.Service.ZnuSectorDetailService
{
    public class ZnuSectorDetailService : IZnuSectorDetailService
    {
        private readonly IZnuSectorDetailRepo _repo;

        public ZnuSectorDetailService(IZnuSectorDetailRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ZnuSectorDetailReadDto>> GetAllAsync()
        {
            var details = await _repo.GetAllAsync();
            return details.Select(d => new ZnuSectorDetailReadDto
            {
                Id = d.Id,
                Description = d.Description,
                DepartmentId = d.DepartmentId
            });
        }

        public async Task<ZnuSectorDetailReadDto?> GetByIdAsync(int id)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d == null) return null;

            return new ZnuSectorDetailReadDto
            {
                Id = d.Id,
                Description = d.Description,
                DepartmentId = d.DepartmentId
            };
        }

        public async Task AddAsync(ZnuSectorDetailCreateDto dto)
        {
            var detail = new ZnuSectorDetail
            {
                Description = dto.Description,
                DepartmentId = dto.DepartmentId
            };

            await _repo.AddAsync(detail);
            await _repo.SaveChangesAsync();
        }
    }

}

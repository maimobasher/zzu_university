using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Sector;
using zzu_university.data.Repository.ZnuSectorDepartmentRepo;
using zzu_university.domain.DTOS.SectorDto;

namespace zzu_university.domain.Service.ZnuSectorDepartmentService
{
    public class ZnuSectorDepartmentService : IZnuSectorDepartmentService
    {
        private readonly IZnuSectorDepartmentRepo _repo;

        public ZnuSectorDepartmentService(IZnuSectorDepartmentRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ZnuSectorDepartmentReadDto>> GetAllAsync()
        {
            var depts = await _repo.GetAllAsync();
            return depts.Select(d => new ZnuSectorDepartmentReadDto
            {
                Id = d.Id,
                Name = d.Name,
                Title = d.Title,
                Description = d.Description,
                Media_Url = d.Media_Url,
                SectorId = d.SectorId,
                Details = d.Details?.Select(e => new ZnuSectorDetailReadDto
                {
                    Id = e.Id,
                    Description = e.Description,
                    DepartmentId = e.DepartmentId
                }).ToList()
            });
        }

        public async Task<ZnuSectorDepartmentReadDto?> GetByIdAsync(int id)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d == null) return null;

            return new ZnuSectorDepartmentReadDto
            {
                Id = d.Id,
                Name = d.Name,
                Title = d.Title,
                Description = d.Description,
                Media_Url = d.Media_Url,
                SectorId = d.SectorId,
                Details = d.Details?.Select(e => new ZnuSectorDetailReadDto
                {
                    Id = e.Id,
                    Description = e.Description,
                    DepartmentId = e.DepartmentId
                }).ToList()
            };
        }

        public async Task AddAsync(ZnuSectorDepartmentCreateDto dto)
        {
            var dept = new ZnuSectorDepartment
            {
                Name = dto.Name,
                Title = dto.Title,
                Description = dto.Description,
                Media_Url = dto.Media_Url,
                SectorId = dto.SectorId
            };

            await _repo.AddAsync(dept);
            await _repo.SaveChangesAsync();
        }
    }

}

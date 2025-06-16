using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Sector;
using zzu_university.data.Repository.ZnuSectorRepo;
using zzu_university.domain.DTOS.SectorDto;

namespace zzu_university.domain.Service.ZnuSectorService
{
    public class ZnuSectorService : IZnuSectorService
    {
        private readonly IZnuSectorRepo _repo;

        public ZnuSectorService(IZnuSectorRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ZnuSectorReadDto>> GetAllAsync()
        {
            var sectors = await _repo.GetAllAsync();
            return sectors.Select(s => new ZnuSectorReadDto
            {
                Id = s.Id,
                Name = s.Name,
                Departments = s.Departments?.Select(d => new ZnuSectorDepartmentReadDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Title = d.Title,
                    Description = d.Description,
                    Media_Url = d.Media_Url
                }).ToList()
            });
        }

        public async Task<ZnuSectorReadDto?> GetByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            if (s == null) return null;

            return new ZnuSectorReadDto
            {
                Id = s.Id,
                Name = s.Name,
                Departments = s.Departments?.Select(d => new ZnuSectorDepartmentReadDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Title = d.Title,
                    Description = d.Description,
                    Media_Url = d.Media_Url
                }).ToList()
            };
        }

        public async Task AddAsync(ZnuSectorReadDto dto)
        {
            var sector = new ZnuSector
            {
                Name = dto.Name
            };

            await _repo.AddAsync(sector);
            await _repo.SaveChangesAsync();
        }
    }

}

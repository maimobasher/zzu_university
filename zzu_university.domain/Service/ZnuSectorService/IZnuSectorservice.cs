using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.SectorDto;

namespace zzu_university.domain.Service.ZnuSectorService
{
    public interface IZnuSectorService
    {
        Task<IEnumerable<ZnuSectorReadDto>> GetAllAsync();
        Task<ZnuSectorReadDto?> GetByIdAsync(int id);
        Task AddAsync(ZnuSectorReadDto dto);
    }

}

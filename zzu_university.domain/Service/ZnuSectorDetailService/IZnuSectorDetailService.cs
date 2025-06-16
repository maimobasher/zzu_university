using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.SectorDto;

namespace zzu_university.domain.Service.ZnuSectorDetailService
{
    public interface IZnuSectorDetailService
    {
        Task<IEnumerable<ZnuSectorDetailReadDto>> GetAllAsync();
        Task<ZnuSectorDetailReadDto?> GetByIdAsync(int id);
        Task AddAsync(ZnuSectorDetailCreateDto dto);
    }

}

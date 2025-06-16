using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.SectorDto;

namespace zzu_university.domain.Service.ZnuSectorDepartmentService
{
    public interface IZnuSectorDepartmentService
    {
        Task<IEnumerable<ZnuSectorDepartmentReadDto>> GetAllAsync();
        Task<ZnuSectorDepartmentReadDto?> GetByIdAsync(int id);
        Task AddAsync(ZnuSectorDepartmentCreateDto dto);
    }

}

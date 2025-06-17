using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.ManagementType.zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.ManagementTypeService
{
    public interface IManagementTypeService
    {
        Task<IEnumerable<ManagementTypeDto>> GetAllAsync();
        Task<ManagementTypeDto?> GetByIdAsync(int id);
        Task<ManagementTypeDto> AddAsync(ManagementTypeDto dto);
        Task<bool> UpdateAsync(int id, ManagementTypeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

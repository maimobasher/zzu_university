using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.PrivacyDto;

namespace zzu_university.domain.Service.PrivacyService
{
    public interface IPrivacyService
    {
        Task<IEnumerable<PrivacyReadDto>> GetAllAsync();
        Task<PrivacyReadDto> GetByIdAsync(int id);
        Task<PrivacyReadDto> CreateAsync(PrivacyCreateDto dto);
        Task<PrivacyReadDto> UpdateAsync(PrivacyUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }

}

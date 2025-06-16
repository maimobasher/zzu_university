using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.ContactsDto;

namespace zzu_university.domain.Service.ContactService
{
    public interface IZnuContactService
    {
        Task<ZnuContactReadDto> GetAsync();
        Task AddOrUpdateAsync(ZnuContactCreateDto dto);
    }

}

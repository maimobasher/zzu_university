using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.AboutService
{
    public interface IAboutService
    {
        Task<AboutDto> GetAboutAsync();
        Task<AboutDto> GetByIdAsync(int id);
        Task CreateAboutAsync(AboutDto aboutDto);
        Task UpdateAboutAsync(AboutDto aboutDto);
        Task DeleteAboutAsync(int id); // ← لازم يكون فيها id
    }
}

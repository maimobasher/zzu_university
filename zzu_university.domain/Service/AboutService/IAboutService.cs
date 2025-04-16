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
        Task CreateAboutAsync(AboutDto aboutDto);
        Task UpdateAboutAsync(AboutDto aboutDto);
        Task DeleteAboutAsync();
    }
}

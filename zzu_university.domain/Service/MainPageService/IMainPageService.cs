using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.MainPage;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.MainPageService
{
    public interface IMainPageService
    {
        Task<MainPage> GetMainPageAsync();
        Task<MainPage> CreateMainPageAsync(MainPageDto mainPageDto);
        Task<bool> UpdateMainPageAsync(MainPageDto mainPageDto);
        Task<bool> DeleteMainPageAsync(int id);
    }
}

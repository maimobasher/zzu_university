using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.MainPage;
using zzu_university.data.Repository.AboutRepo;

namespace zzu_university.data.Repository.MainPageRepo
{
    public interface IMainPageRepo : IRepo<MainPage,int>
    {
        Task<MainPage> GetMainPageAsync();
        Task AddMainPageAsync(MainPage mainPage);
        void UpdateMainPage(MainPage mainPage);
        void DeleteMainPage(MainPage mainPage);
        //Task GetAsync();
    }
}

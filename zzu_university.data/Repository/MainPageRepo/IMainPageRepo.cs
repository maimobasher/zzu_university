using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.MainPage;
using zzu_university.data.Repository.AboutRepo;

namespace zzu_university.data.Repository.MainPageRepo
{
    public interface IMainPageRepo : IRepo<MainPage, int>
    {
        Task<MainPage> GetMainPageAsync();                      // Gets default or primary main page
        Task<MainPage> GetMainPageAsyncById(int id);            // Gets main page by specific ID
        Task AddMainPageAsync(MainPage mainPage);               // Adds a new main page
        Task UpdateMainPageById(int id, MainPage mainPage);     // Updates main page by ID
        Task DeleteMainPageById(int id);                        // Deletes main page by ID
    }
}

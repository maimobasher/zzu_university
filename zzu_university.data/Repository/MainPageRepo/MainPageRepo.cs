using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.MainPage;
using zzu_university.data.Repository.MainRepo;

namespace zzu_university.data.Repository.MainPageRepo
{
    public class MainPageRepo : Repo<MainPageRepo, int>, IMainPageRepo
    {
        private readonly ApplicationDbContext _context;

        

        public async Task<MainPage> GetMainPageAsync()
        {
            return await _context.MainPages.FirstOrDefaultAsync();
        }

        public async Task AddMainPageAsync(MainPage mainPage)
        {
            await _context.MainPages.AddAsync(mainPage);
        }

        public void UpdateMainPage(MainPage mainPage)
        {
            _context.MainPages.Update(mainPage);
        }

        public void DeleteMainPage(MainPage mainPage)
        {
            _context.MainPages.Remove(mainPage);
        }

        //Task IMainPageRepo.GetAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public MainPageRepo(ApplicationDbContext context) :base(context)
        { 
            _context = context;
        }   
    }
}

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.MainPage;
using zzu_university.data.Repository.MainRepo;

namespace zzu_university.data.Repository.MainPageRepo
{
    public class MainPageRepo : Repo<MainPage, int>, IMainPageRepo
    {
        private readonly ApplicationDbContext _context;

        public MainPageRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MainPage> GetMainPageAsync()
        {
            return await _context.MainPages.FirstOrDefaultAsync();
        }

        public async Task<MainPage> GetMainPageAsyncById(int id)
        {
            return await _context.MainPages.FindAsync(id);
        }

        public async Task AddMainPageAsync(MainPage mainPage)
        {
            await _context.MainPages.AddAsync(mainPage);
        }

        public async Task UpdateMainPageById(int id, MainPage mainPage)
        {
            var existing = await _context.MainPages.FindAsync(id);
            if (existing == null) return;

            // You can manually map the properties or use AutoMapper if configured
            existing.Title = mainPage.Title;
            existing.Description = mainPage.Description;
            existing.ImageUrl = mainPage.ImageUrl;
            // Add other fields as needed

            _context.MainPages.Update(existing);
        }

        public async Task DeleteMainPageById(int id)
        {
            var existing = await _context.MainPages.FindAsync(id);
            if (existing != null)
            {
                _context.MainPages.Remove(existing);
            }
        }
    }
}

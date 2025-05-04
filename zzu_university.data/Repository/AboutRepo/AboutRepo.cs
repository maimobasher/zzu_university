using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.About;
using zzu_university.data.Repository.MainRepo;

namespace zzu_university.data.Repository.AboutRepo
{
    public class AboutRepo : Repo<About, int>, IAboutRepo
    {
        private readonly ApplicationDbContext _context;

        public AboutRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<About> GetAsync()
        {
            return await _context.Abouts.FirstOrDefaultAsync();
        }

        public async Task AddAsync(About about)
        {
            await _context.Abouts.AddAsync(about);
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _context.Abouts.FindAsync(id);
        }

        public async Task UpdateAboutAsync(int id, About about)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null) return;

            existing.Title = about.Title;
            existing.Description = about.Description;
            existing.Vision = about.Vision;
            existing.Mission = about.Mission;
            existing.History = about.History;
            existing.ContactEmail = about.ContactEmail;
            existing.PhoneNumber = about.PhoneNumber;
            existing.Address = about.Address;

            _context.Abouts.Update(existing);
        }

        public async Task DeleteAboutAsync(int id)
        {
            var about = await GetByIdAsync(id);
            if (about != null)
            {
                _context.Abouts.Remove(about);
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //public AboutRepo(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<About> GetAsync()
        {
            return await _context.Abouts.FirstOrDefaultAsync();
        }

        public async Task? AddAsync(About about)
        {
            await _context.Abouts.AddAsync(about);
        }

        public void Update(About about)
        {
            _context.Abouts.Update(about);
        }

        public void Delete(About about)
        {
            _context.Abouts.Remove(about);
        }
        public AboutRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }

   
}

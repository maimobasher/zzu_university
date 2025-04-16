using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.News;

namespace zzu_university.data.Repository.NewsRepo
{
    public class NewsRepo:MainRepo.Repo<News,int>,INewsRepo
    {
        public NewsRepo(ApplicationDbContext context):base(context) 
        { 
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        //public NewsRepo(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News?> GetByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task AddAsync(News news)
        {
            await _context.News.AddAsync(news);
        }

        public void Update(News news)
        {
            _context.News.Update(news);
        }

        public void Delete(News news)
        {
            _context.News.Remove(news);
        }

    }
}

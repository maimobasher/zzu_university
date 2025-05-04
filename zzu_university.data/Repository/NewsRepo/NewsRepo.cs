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
    public class NewsRepo : MainRepo.Repo<News, int>, INewsRepo
    {
        private readonly ApplicationDbContext _context;

        public NewsRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

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

        public async Task UpdateAsyncById(int id, News news)
        {
            var existingNews = await _context.News.FindAsync(id);
            if (existingNews != null)
            {
                existingNews.Title = news.Title;
                existingNews.Content = news.Content;
                existingNews.PublishedDate = news.PublishedDate;
                existingNews.ImageUrl = news.ImageUrl;

                _context.News.Update(existingNews);
            }
        }

        public async Task DeleteAsyncById(int id)
        {
            var existingNews = await _context.News.FindAsync(id);
            if (existingNews != null)
            {
                _context.News.Remove(existingNews);
            }
        }
    }
}

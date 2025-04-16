using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.News;
using zzu_university.data.Repository.UnitOfWork;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.NewsService
{
    public class NewsService:INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
        {
            var newsList = await _unitOfWork.News.GetAllAsync();
            return newsList.Select(n => new NewsDto
            {
                Title = n.Title,
                Content = n.Content,
                PublishedDate = n.PublishedDate,
                ImageUrl = n.ImageUrl
            });
        }

        public async Task<NewsDto?> GetNewsByIdAsync(int id)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news == null) return null;

            return new NewsDto
            {
                Title = news.Title,
                Content = news.Content,
                PublishedDate = news.PublishedDate,
                ImageUrl = news.ImageUrl
            };
        }
        public async Task<bool> UpdateNewsAsync(int id, NewsDto newsDto)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news == null)
                return false;

            news.Title = newsDto.Title;
            news.Content = newsDto.Content;
            // إذا لم يتم تمرير تاريخ نشر، يتم استخدام الوقت الحالي (UTC)
            news.PublishedDate = newsDto.PublishedDate != default(DateTime) ? newsDto.PublishedDate : DateTime.UtcNow;
            news.ImageUrl = newsDto.ImageUrl;

            _unitOfWork.News.Update(news);
            _unitOfWork.Save();
            return true;
        }
        public async Task<bool> DeleteNewsAsync(int id)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news == null)
                return false;

            _unitOfWork.News.Delete(news);
            _unitOfWork.Save();
            return true;
        }

        public async Task<News> CreateNewsAsync(NewsDto newsDto)
        {
            var news = new News
            {
                Title = newsDto.Title,
                Content = newsDto.Content,
                PublishedDate = newsDto.PublishedDate != default(DateTime) ? newsDto.PublishedDate : DateTime.UtcNow,
                ImageUrl = newsDto.ImageUrl
            };

            await _unitOfWork.News.AddAsync(news);
            _unitOfWork.Save();
            return news;
        }
    }
}
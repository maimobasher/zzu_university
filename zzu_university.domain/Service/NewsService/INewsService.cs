using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.News;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.NewsService
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAllNewsAsync();
        Task<NewsDto?> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(NewsDto newsDto);
        Task<bool> UpdateNewsAsync(int id, NewsDto newsDto);
        Task<bool> DeleteNewsAsync(int id);
    }
}

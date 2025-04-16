using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.News;
using zzu_university.data.Repository.AboutRepo;

namespace zzu_university.data.Repository.NewsRepo
{
    public interface INewsRepo:IRepo<NewsRepo,int>
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News?> GetByIdAsync(int id);
        Task AddAsync(News news);
        void Update(News news);
        void Delete(News news);
    }
}

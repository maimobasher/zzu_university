using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.News;
using zzu_university.data.Repository.AboutRepo;

namespace zzu_university.data.Repository.NewsRepo
{
    public interface INewsRepo : IRepo<NewsRepo, int>
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News?> GetByIdAsync(int id);
        Task AddAsync(News news);
        Task UpdateAsyncById(int id, News news);  // Updated method for async update by ID
        Task DeleteAsyncById(int id);  // Updated method for async delete by ID
    }

}

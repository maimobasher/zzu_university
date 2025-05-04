using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.About;
using zzu_university.data.Repository.MainRepo;

namespace zzu_university.data.Repository.AboutRepo
{
    public interface IAboutRepo : IRepo<About, int>
    {
        Task<About> GetAsync();                   // Optional: Get first or default
        Task AddAsync(About about);
        Task<About> GetByIdAsync(int id);
        Task UpdateAboutAsync(int id, About about);  // ← متوقعة هنا
        Task DeleteAboutAsync(int id);               // ← متوقعة هنا
    }


    public interface IRepo<T1, T2>
    {
    }
}

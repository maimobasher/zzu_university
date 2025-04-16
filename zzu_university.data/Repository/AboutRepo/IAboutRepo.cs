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
        Task<About> GetAsync();
        Task AddAsync(About about);
        void Update(About about);
        void Delete(About about);
    }

    public interface IRepo<T1, T2>
    {
    }
}

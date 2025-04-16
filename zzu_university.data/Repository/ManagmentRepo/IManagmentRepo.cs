using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model;
using zzu_university.data.Repository.AboutRepo;

namespace zzu_university.data.Repository.ManagmentRepo
{
    public interface IManagmentRepo:IRepo<Managment,int>
    {
        Task<Managment> GetAsync();
        Task AddAsync(Managment managment);
        void Update(Managment managment);
        void Delete(Managment managment);
    }
}

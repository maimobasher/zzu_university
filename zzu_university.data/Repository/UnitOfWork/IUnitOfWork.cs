using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Repository.AboutRepo;
using zzu_university.data.Repository.MainPageRepo;
using zzu_university.data.Repository.ManagmentRepo;
using zzu_university.data.Repository.NewsRepo;
using zzu_university.data.Repository.ServiceRepo;
using zzu_university.data.Repository.UserRepo;

namespace zzu_university.data.Repository.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IAboutRepo About {  get; }
        IMainPageRepo MainPage { get; }
        IManagmentRepo Managment { get; }
        INewsRepo News { get; }
        IServiceRepo Service { get; }
        IUserRepo User { get; }
        int save();
        void Save();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Repository.AboutRepo;
using zzu_university.data.Repository.MainPageRepo;
using zzu_university.data.Repository.ManagmentRepo;
using zzu_university.data.Repository.NewsRepo;
using zzu_university.data.Repository.ServiceRepo;
using zzu_university.data.Repository.UserRepo;

namespace zzu_university.data.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAboutRepo About { get; private set; }
        public IMainPageRepo MainPage { get; private set; }
        public IManagmentRepo Managment { get; private set; }
        public INewsRepo News { get; private set; }
        public IServiceRepo Service { get; private set; }
        public IUserRepo User { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            About = new AboutRepo.AboutRepo(_context);
            MainPage = new MainPageRepo.MainPageRepo(_context);
            Managment=new ManagmentRepo.ManagmentRepo(_context);
            News=new NewsRepo.NewsRepo(_context);
            Service=new ServiceRepo.ServiceRepo(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public int save()
        {
            throw new NotImplementedException();
        }

        void IUnitOfWork.Save()
        {
            throw new NotImplementedException();
        }
    }
}

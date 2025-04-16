using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.Services;

namespace zzu_university.data.Repository.ServiceRepo
{
    public class ServiceRepo:MainRepo.Repo<Service,int>,IServiceRepo
    {
        public ServiceRepo(ApplicationDbContext context ):base(context)
        {
            _context = context;

        }
        private readonly ApplicationDbContext _context;

        //public ServicesRepo(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }

        public void Update(Service service)
        {
            _context.Services.Update(service);
        }

        public void Delete(Service service)
        {
            _context.Services.Remove(service);
        }
    }
}

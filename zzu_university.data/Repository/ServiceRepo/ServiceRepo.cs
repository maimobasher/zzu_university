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
    public class ServiceRepo : MainRepo.Repo<Service, int>, IServiceRepo
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

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

        public async Task UpdateAsyncById(int id, Service service)
        {
            var existingService = await _context.Services.FindAsync(id);
            if (existingService != null)
            {
                existingService.Name = service.Name;
                existingService.Description = service.Description;
                existingService.IconUrl = service.IconUrl; // Add more properties as required

                _context.Services.Update(existingService);
            }
        }

        public async Task DeleteAsyncById(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
        }
    }
}

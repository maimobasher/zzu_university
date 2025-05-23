﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Services;
using zzu_university.data.Repository.AboutRepo;

namespace zzu_university.data.Repository.ServiceRepo
{
    public interface IServiceRepo : IRepo<Service, int>
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task AddAsync(Service service);
        Task UpdateAsyncById(int id, Service service);  // Updated to asynchronous method
        Task DeleteAsyncById(int id);  // Updated to asynchronous method
    }

}

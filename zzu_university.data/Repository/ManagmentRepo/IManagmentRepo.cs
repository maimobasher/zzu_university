using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model;
using zzu_university.data.Repository.AboutRepo;

namespace zzu_university.data.Repository.ManagmentRepo
{
    public interface IManagmentRepo : IRepo<Managment, int>
    {
        Task<Managment> GetAsync(); // Get the first or default record (general)
        Task<Managment> GetAsyncById(int id); // Get by specific ID

        Task AddAsync(Managment managment); // Add new record

        Task UpdateAsyncById(int id, Managment managment); // Update record by ID

        Task DeleteAsyncById(int id); // Delete record by ID
    }

}

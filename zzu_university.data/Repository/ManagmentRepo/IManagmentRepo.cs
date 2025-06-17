using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model;
using zzu_university.data.Repository.AboutRepo;

namespace zzu_university.data.Repository.ManagmentRepo
{
    public interface IManagmentRepo : IRepo<Management, int>
    {
        // Get all managements including their type
        Task<IEnumerable<Management>> GetAllWithTypeAsync();

        // Get one management with its type by ID
        Task<Management?> GetWithTypeByIdAsync(int id);

        // Optional general purpose fetch
        Task<Management?> GetDefaultAsync(); // e.g., first or most used

        // Add new management
        Task AddAsync(Management management);

        // Update existing management by ID
        Task UpdateAsyncById(int id, Management updatedManagement);

        // Delete management by ID
        Task DeleteAsyncById(int id);
    }
}

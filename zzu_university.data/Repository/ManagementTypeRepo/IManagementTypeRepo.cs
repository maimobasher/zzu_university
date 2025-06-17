using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Managment;

namespace zzu_university.data.Repository.ManagementTypeRepo
{
    public interface IManagementTypeRepo
    {
        Task<IEnumerable<ManagementType>> GetAllAsync();
        Task<ManagementType?> GetByIdAsync(int id);
        Task AddAsync(ManagementType type);
        Task UpdateAsync(int id, ManagementType updatedType);
        Task DeleteAsync(int id);
    }
}

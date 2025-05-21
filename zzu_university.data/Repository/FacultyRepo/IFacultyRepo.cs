using System.Collections.Generic;
using System.Threading.Tasks;
using zzu_university.data.Model.Faculty;

namespace zzu_university.data.Repository
{
    public interface IFacultyRepo
    {
        Task<IEnumerable<Faculty>> GetAllAsync();
        Task<Faculty> GetByIdAsync(int id);
        Task<Faculty> AddAsync(Faculty faculty);
        Task<Faculty> UpdateAsync(Faculty faculty);
        Task<bool> DeleteAsync(int id);
    }
}

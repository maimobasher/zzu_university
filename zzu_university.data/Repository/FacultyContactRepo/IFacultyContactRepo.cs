using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.FacultyContact;

namespace zzu_university.data.Repository.FacultyContactRepo
{
    public interface IFacultyContactRepo
    {
        Task<IEnumerable<FacultyContact>> GetAllAsync();
        Task<FacultyContact> GetByIdAsync(int id);
        Task AddAsync(FacultyContact contact);
        Task<bool> DeleteAsync(int id);
        Task SaveAsync();
    }
}

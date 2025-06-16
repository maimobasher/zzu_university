using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Sector;

namespace zzu_university.data.Repository.ZnuSectorDepartmentRepo
{
    public interface IZnuSectorDepartmentRepo
    {
        Task<IEnumerable<ZnuSectorDepartment>> GetAllAsync();
        Task<ZnuSectorDepartment?> GetByIdAsync(int id);
        Task AddAsync(ZnuSectorDepartment department);
        Task<bool> SaveChangesAsync();
    }

}

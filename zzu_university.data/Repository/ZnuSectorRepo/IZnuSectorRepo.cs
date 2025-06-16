using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Sector;

namespace zzu_university.data.Repository.ZnuSectorRepo
{
    public interface IZnuSectorRepo
    {
        Task<IEnumerable<ZnuSector>> GetAllAsync();
        Task<ZnuSector?> GetByIdAsync(int id);
        Task AddAsync(ZnuSector sector);
        Task<bool> SaveChangesAsync();
    }

}

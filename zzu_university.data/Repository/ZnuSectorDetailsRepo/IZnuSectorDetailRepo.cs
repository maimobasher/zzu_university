using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Sector;

namespace zzu_university.data.Repository.ZnuSectorDetailsRepo
{
    public interface IZnuSectorDetailRepo
    {
        Task<IEnumerable<ZnuSectorDetail>> GetAllAsync();
        Task<ZnuSectorDetail?> GetByIdAsync(int id);
        Task AddAsync(ZnuSectorDetail detail);
        Task<bool> SaveChangesAsync();
    }

}

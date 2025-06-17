using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Privacy;

namespace zzu_university.data.Repository.PrivacyRepo
{
    public interface IPrivacyRepo
    {
        Task<IEnumerable<Privacy>> GetAllAsync();
        Task<Privacy> GetByIdAsync(int id);
        Task<Privacy> AddAsync(Privacy item);
        Task<Privacy> UpdateAsync(Privacy item);
        Task<bool> DeleteAsync(int id);
    }
}

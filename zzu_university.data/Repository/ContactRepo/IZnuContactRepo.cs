using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Contacts;

namespace zzu_university.data.Repository.ContactRepo
{
    public interface IZnuContactRepo
    {
        Task<ZnuContact> GetAsync();
        Task AddOrUpdateAsync(ZnuContact contact);
        Task SaveChangesAsync();
    }

}

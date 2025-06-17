using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.FAQS;

namespace zzu_university.data.Repository.FaqRepo
{
    public interface IFaqRepo
    {
        Task<IEnumerable<FAQ>> GetAllAsync();
        Task<FAQ> GetByIdAsync(int id);
        Task<FAQ> AddAsync(FAQ faq);
        Task<FAQ> UpdateAsync(FAQ faq);
        Task<bool> DeleteAsync(int id);
    }
}

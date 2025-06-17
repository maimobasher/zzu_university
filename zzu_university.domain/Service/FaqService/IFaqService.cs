using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.FAQDto;

namespace zzu_university.domain.Service.FaqService
{
    public interface IFaqService
    {
        Task<IEnumerable<FaqReadDto>> GetAllAsync();
        Task<FaqReadDto> GetByIdAsync(int id);
        Task<FaqReadDto> AddAsync(FaqCreateUpdateDto faqCreateDto);
        Task<FaqReadDto> UpdateAsync(int id, FaqCreateUpdateDto faqCreateDto);
        Task<bool> DeleteAsync(int id);
    }
}

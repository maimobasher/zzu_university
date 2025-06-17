using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.FAQS;
using zzu_university.data.Repository.FaqRepo;
using zzu_university.domain.DTOS.FAQDto;

namespace zzu_university.domain.Service.FaqService
{
    public class FaqService : IFaqService
    {
        private readonly IFaqRepo _faqRepo;

        public FaqService(IFaqRepo faqRepo)
        {
            _faqRepo = faqRepo;
        }

        public async Task<IEnumerable<FaqReadDto>> GetAllAsync()
        {
            var faqs = await _faqRepo.GetAllAsync();
            return faqs.Select(f => new FaqReadDto
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
            });
        }

        public async Task<FaqReadDto> GetByIdAsync(int id)
        {
            var faq = await _faqRepo.GetByIdAsync(id);
            if (faq == null) return null;

            return new FaqReadDto
            {
                Id = faq.Id,
                Question = faq.Question,
                Answer = faq.Answer
            };
        }

        public async Task<FaqReadDto> AddAsync(FaqCreateUpdateDto dto)
        {
            var faq = new FAQ
            {
                Question = dto.Question,
                Answer = dto.Answer
            };

            var added = await _faqRepo.AddAsync(faq);

            return new FaqReadDto
            {
                Id = added.Id,
                Question = added.Question,
                Answer = added.Answer
            };
        }

        public async Task<FaqReadDto> UpdateAsync(int id, FaqCreateUpdateDto dto)
        {
            var existing = await _faqRepo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Question = dto.Question;
            existing.Answer = dto.Answer;

            var updated = await _faqRepo.UpdateAsync(existing);

            return new FaqReadDto
            {
                Id = updated.Id,
                Question = updated.Question,
                Answer = updated.Answer
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _faqRepo.DeleteAsync(id);
        }
    }
}

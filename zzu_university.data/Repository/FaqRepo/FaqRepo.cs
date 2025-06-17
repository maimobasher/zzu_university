using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.FAQS;

namespace zzu_university.data.Repository.FaqRepo
{
    public class FaqRepo : IFaqRepo
    {
        private readonly ApplicationDbContext _context;

        public FaqRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FAQ>> GetAllAsync()
        {
            return await _context.FAQs.ToListAsync();
        }

        public async Task<FAQ> GetByIdAsync(int id)
        {
            return await _context.FAQs.FindAsync(id);
        }

        public async Task<FAQ> AddAsync(FAQ faq)
        {
            await _context.FAQs.AddAsync(faq);
            await _context.SaveChangesAsync();
            return faq;
        }

        public async Task<FAQ> UpdateAsync(FAQ faq)
        {
            var existingFaq = await _context.FAQs.FindAsync(faq.Id);
            if (existingFaq == null) return null;

            existingFaq.Question = faq.Question;
            existingFaq.Answer = faq.Answer;

            _context.FAQs.Update(existingFaq);
            await _context.SaveChangesAsync();
            return existingFaq;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var faq = await _context.FAQs.FindAsync(id);
            if (faq == null) return false;

            _context.FAQs.Remove(faq);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Privacy;

namespace zzu_university.data.Repository.PrivacyRepo
{
    public class PrivacyRepo : IPrivacyRepo
    {
        private readonly ApplicationDbContext _context;

        public PrivacyRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Privacy>> GetAllAsync()
        {
            return await _context.Privacy.ToListAsync();
        }

        public async Task<Privacy> GetByIdAsync(int id)
        {
            return await _context.Privacy.FindAsync(id);
        }

        public async Task<Privacy> AddAsync(Privacy item)
        {
            _context.Privacy.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Privacy> UpdateAsync(Privacy item)
        {
            var existing = await _context.Privacy.FindAsync(item.Id);
            if (existing == null)
                return null;

            existing.Title = item.Title;
            existing.Content = item.Content;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Privacy.FindAsync(id);
            if (existing == null)
                return false;

            _context.Privacy.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

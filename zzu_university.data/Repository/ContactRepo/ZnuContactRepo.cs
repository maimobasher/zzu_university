using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Contacts;

namespace zzu_university.data.Repository.ContactRepo
{
    public class ZnuContactRepo : IZnuContactRepo
    {
        private readonly ApplicationDbContext _context;

        public ZnuContactRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ZnuContact> GetAsync()
        {
            // Assuming only one record for university contact info
            return await _context.ZnuContacts.FirstOrDefaultAsync();
        }

        public async Task AddOrUpdateAsync(ZnuContact contact)
        {
            var existing = await _context.ZnuContacts.FirstOrDefaultAsync();

            if (existing == null)
            {
                await _context.ZnuContacts.AddAsync(contact);
            }
            else
            {
                existing.Address = contact.Address;
                existing.Email = contact.Email;
                existing.Phone = contact.Phone;
                _context.ZnuContacts.Update(existing);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

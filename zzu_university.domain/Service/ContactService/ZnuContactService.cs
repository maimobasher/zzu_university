using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Contacts;
using zzu_university.data.Repository.ContactRepo;
using zzu_university.domain.DTOS.ContactsDto;

namespace zzu_university.domain.Service.ContactService
{
    public class ZnuContactService : IZnuContactService
    {
        private readonly IZnuContactRepo _repo;

        public ZnuContactService(IZnuContactRepo repo)
        {
            _repo = repo;
        }

        public async Task<ZnuContactReadDto> GetAsync()
        {
            var contact = await _repo.GetAsync();

            if (contact == null)
                return null;

            return new ZnuContactReadDto
            {
                Address = contact.Address,
                Email = contact.Email,
                Phone = contact.Phone
            };
        }

        public async Task AddOrUpdateAsync(ZnuContactCreateDto dto)
        {
            var contact = new ZnuContact
            {
                Address = dto.Address,
                Email = dto.Email,
                Phone = dto.Phone
            };

            await _repo.AddOrUpdateAsync(contact);
            await _repo.SaveChangesAsync();
        }
    }
}

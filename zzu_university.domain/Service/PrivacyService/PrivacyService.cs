using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Privacy;
using zzu_university.data.Repository.PrivacyRepo;
using zzu_university.domain.DTOS.PrivacyDto;

namespace zzu_university.domain.Service.PrivacyService
{
    public class PrivacyService : IPrivacyService
    {
        private readonly IPrivacyRepo _privacyRepo;

        public PrivacyService(IPrivacyRepo privacyRepo)
        {
            _privacyRepo = privacyRepo;
        }

        public async Task<IEnumerable<PrivacyReadDto>> GetAllAsync()
        {
            var items = await _privacyRepo.GetAllAsync();
            return items.Select(p => new PrivacyReadDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content
            });
        }

        public async Task<PrivacyReadDto> GetByIdAsync(int id)
        {
            var item = await _privacyRepo.GetByIdAsync(id);
            if (item == null) return null;

            return new PrivacyReadDto
            {
                Id = item.Id,
                Title = item.Title,
                Content = item.Content
            };
        }

        public async Task<PrivacyReadDto> CreateAsync(PrivacyCreateDto dto)
        {
            var model = new Privacy
            {
                Title = dto.Title,
                Content = dto.Content
            };

            var created = await _privacyRepo.AddAsync(model);

            return new PrivacyReadDto
            {
                Id = created.Id,
                Title = created.Title,
                Content = created.Content
            };
        }

        public async Task<PrivacyReadDto> UpdateAsync(PrivacyUpdateDto dto)
        {
            var updated = await _privacyRepo.UpdateAsync(new Privacy
            {
                Id = dto.Id,
                Title = dto.Title,
                Content = dto.Content
            });

            if (updated == null) return null;

            return new PrivacyReadDto
            {
                Id = updated.Id,
                Title = updated.Title,
                Content = updated.Content
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _privacyRepo.DeleteAsync(id);
        }
    }
}
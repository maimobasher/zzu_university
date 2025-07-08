using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zzu_university.data.Model.Certificate;
using zzu_university.data.Repository.CertificateRepo;
using zzu_university.domain.DTOS.CertificateDto;

namespace zzu_university.domain.Service.CertificateService
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepo _repository;

        public CertificateService(ICertificateRepo repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CertificateReadDto>> GetAllAsync(bool includeDeleted = false, bool onlyDeleted = false)
        {
            var certificates = await _repository.GetAllAsync();

            if (onlyDeleted)
                certificates = certificates.Where(c => c.is_deleted).ToList();
            else if (!includeDeleted)
                certificates = certificates.Where(c => !c.is_deleted).ToList();

            return certificates.Select(c => new CertificateReadDto
            {
                Id = c.Id,
                CertificateName = c.CertificateName,
                IssueDate = c.IssueDate,
                Description = c.Description,
                is_deleted = c.is_deleted
            });
        }

        public async Task<CertificateReadDto> GetByIdAsync(int id)
        {
            var cert = await _repository.GetByIdAsync(id);
            if (cert == null)
                return null;

            return new CertificateReadDto
            {
                Id = cert.Id,
                CertificateName = cert.CertificateName,
                IssueDate = cert.IssueDate,
                Description = cert.Description,
                is_deleted = cert.is_deleted
            };
        }

        public async Task<CertificateReadDto> CreateAsync(CertificateCreateDto dto)
        {
            var cert = new Certificate
            {
                CertificateName = dto.CertificateName,
                IssueDate = dto.IssueDate,
                Description = dto.Description,
                is_deleted = false
            };

            await _repository.AddAsync(cert);

            return new CertificateReadDto
            {
                Id = cert.Id,
                CertificateName = cert.CertificateName,
                IssueDate = cert.IssueDate,
                Description = cert.Description,
                is_deleted = cert.is_deleted
            };
        }

        public async Task<CertificateReadDto> UpdateAsync(CertificateUpdateDto dto)
        {
            var cert = new Certificate
            {
                Id = dto.Id,
                CertificateName = dto.CertificateName,
                IssueDate = dto.IssueDate,
                Description = dto.Description,
                is_deleted = dto.is_deleted
            };

            var updated = await _repository.UpdateAsync(cert);
            if (updated == null)
                return null;

            return new CertificateReadDto
            {
                Id = updated.Id,
                CertificateName = updated.CertificateName,
                IssueDate = updated.IssueDate,
                Description = updated.Description,
                is_deleted = updated.is_deleted
            };
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var cert = await _repository.GetByIdAsync(id);
            if (cert == null || cert.is_deleted)
                return false;

            cert.is_deleted = true;
            var result = await _repository.UpdateAsync(cert);
            return result != null;
        }

        public async Task<bool> RestoreAsync(int id)
        {
            var cert = await _repository.GetByIdAsync(id);
            if (cert == null || !cert.is_deleted)
                return false;

            cert.is_deleted = false;
            var result = await _repository.UpdateAsync(cert);
            return result != null;
        }

        public async Task<bool> HardDeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await HardDeleteAsync(id);
        }
    }
}

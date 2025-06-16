using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<CertificateReadDto>> GetAllAsync()
        {
            var certificates = await _repository.GetAllAsync();
            return certificates.Select(MapToReadDto);
        }

        public async Task<CertificateReadDto> GetByIdAsync(int id)
        {
            var certificate = await _repository.GetByIdAsync(id);
            return certificate == null ? null : MapToReadDto(certificate);
        }

        public async Task<IEnumerable<CertificateReadDto>> GetByStudentIdAsync(int studentId)
        {
            var certificates = await _repository.GetByStudentIdAsync(studentId);
            return certificates.Select(MapToReadDto);
        }

        public async Task<CertificateReadDto> CreateAsync(CertificateCreateDto dto)
        {
            var certificate = new Certificate
            {
                //StudentId = dto.StudentId,
                CertificateName = dto.CertificateName,
                IssueDate = dto.IssueDate,
                Description = dto.Description
            };

            var result = await _repository.AddAsync(certificate);
            return MapToReadDto(result);
        }

        public async Task<CertificateReadDto> UpdateAsync(CertificateUpdateDto dto)
        {
            var certificate = new Certificate
            {
                Id = dto.Id,
                //StudentId = dto.StudentId,
                CertificateName = dto.CertificateName,
                IssueDate = dto.IssueDate,
                Description = dto.Description
            };

            var result = await _repository.UpdateAsync(certificate);
            return MapToReadDto(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        // ✅ Manual Mapper Method
        private CertificateReadDto MapToReadDto(Certificate cert)
        {
            return new CertificateReadDto
            {
                Id = cert.Id,
                //StudentId = cert.StudentId,
                CertificateName = cert.CertificateName,
                IssueDate = cert.IssueDate,
                Description = cert.Description,
               
            };
        }
    }
}

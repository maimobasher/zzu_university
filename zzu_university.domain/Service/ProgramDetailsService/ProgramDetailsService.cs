using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.ProgramDetails.zzu_university.data.Model.Program;
using zzu_university.data.Repository.ProgramDetailsRepo;
using zzu_university.domain.DTOS.ProgramDetails;

namespace zzu_university.domain.Service.ProgramDetailsService
{
    public class ProgramDetailsService : IProgramDetailsService
    {
        private readonly IProgramDetailsRepo _repo;

        public ProgramDetailsService(IProgramDetailsRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProgramDetailsReadDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(p => new ProgramDetailsReadDto
            {
                Id = p.Id,
                ProgramId = p.ProgramId,
                AdmissionRequirements = p.AdmissionRequirements,
                Bylaw = p.Bylaw,
                TuitionFees = p.TuitionFees,
                Courses = p.Courses,
                Files = p.Files,
                ContactInfo = p.ContactInfo
            });
        }

        public async Task<ProgramDetailsReadDto> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            return new ProgramDetailsReadDto
            {
                Id = p.Id,
                ProgramId = p.ProgramId,
                AdmissionRequirements = p.AdmissionRequirements,
                Bylaw = p.Bylaw,
                TuitionFees = p.TuitionFees,
                Courses = p.Courses,
                Files = p.Files,
                ContactInfo = p.ContactInfo
            };
        }

        public async Task<ProgramDetailsReadDto> GetByProgramIdAsync(int programId)
        {
            var p = await _repo.GetByProgramIdAsync(programId);
            if (p == null) return null;

            return new ProgramDetailsReadDto
            {
                Id = p.Id,
                ProgramId = p.ProgramId,
                AdmissionRequirements = p.AdmissionRequirements,
                Bylaw = p.Bylaw,
                TuitionFees = p.TuitionFees,
                Courses = p.Courses,
                Files = p.Files,
                ContactInfo = p.ContactInfo
            };
        }

        public async Task<ProgramDetailsReadDto> CreateAsync(ProgramDetailsCreateDto dto)
        {
            var entity = new ProgramDetails
            {
                ProgramId = dto.ProgramId,
                AdmissionRequirements = dto.AdmissionRequirements,
                Bylaw = dto.Bylaw,
                TuitionFees = dto.TuitionFees,
                Courses = dto.Courses,
                Files = dto.Files,
                ContactInfo = dto.ContactInfo
            };

            var created = await _repo.AddAsync(entity);

            return new ProgramDetailsReadDto
            {
                Id = created.Id,
                ProgramId = created.ProgramId,
                AdmissionRequirements = created.AdmissionRequirements,
                Bylaw = created.Bylaw,
                TuitionFees = created.TuitionFees,
                Courses = created.Courses,
                Files = created.Files,
                ContactInfo = created.ContactInfo
            };
        }

        public async Task<ProgramDetailsReadDto> UpdateAsync(int id, ProgramDetailsCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.ProgramId = dto.ProgramId;
            existing.AdmissionRequirements = dto.AdmissionRequirements;
            existing.Bylaw = dto.Bylaw;
            existing.TuitionFees = dto.TuitionFees;
            existing.Courses = dto.Courses;
            existing.Files = dto.Files;
            existing.ContactInfo = dto.ContactInfo;

            var updated = await _repo.UpdateAsync(existing);

            return new ProgramDetailsReadDto
            {
                Id = updated.Id,
                ProgramId = updated.ProgramId,
                AdmissionRequirements = updated.AdmissionRequirements,
                Bylaw = updated.Bylaw,
                TuitionFees = updated.TuitionFees,
                Courses = updated.Courses,
                Files = updated.Files,
                ContactInfo = updated.ContactInfo
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}

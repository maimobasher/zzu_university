using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.FacultyContact;
using zzu_university.data.Repository.FacultyContactRepo;
using zzu_university.domain.DTOS.FacultyContact;

namespace zzu_university.domain.Service.FacultyContactService
{
    public class FacultyContactService : IFacultyContactService
    {
        private readonly IFacultyContactRepo _repo;
        private readonly ApplicationDbContext _context;

        public FacultyContactService(IFacultyContactRepo repo, ApplicationDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<IEnumerable<FacultyContactReadDto>> GetAllAsync()
        {
            var contacts = await _repo.GetAllAsync();

            return contacts.Select(c => new FacultyContactReadDto
            {
                Id = c.Id,
                Description = c.Description,
                Email = c.Email,
                Phone = c.Phone,
                FacultyName = c.Faculty?.Name ?? "N/A",
                ProgramName = c.Program?.Name ?? "N/A"
            });
        }

        public async Task<FacultyContactReadDto> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;

            return new FacultyContactReadDto
            {
                Id = c.Id,
                Description = c.Description,
                Email = c.Email,
                Phone = c.Phone,
                FacultyName = c.Faculty?.Name ?? "N/A",
                ProgramName = c.Program?.Name ?? "N/A"
            };
        }

        public async Task<FacultyContactReadDto> CreateAsync(FacultyContactCreateDto dto)
        {
            var model = new FacultyContact
            {
                Description = dto.Description,
                Email = dto.Email,
                Phone = dto.Phone,
                FacultyId = dto.FacultyId,
                ProgramId = dto.ProgramId
            };

            await _repo.AddAsync(model);
            await _repo.SaveAsync();

            // Get Faculty & Program names after insert
            var facultyName = await _context.Faculties
                .Where(f => f.FacultyId == dto.FacultyId)
                .Select(f => f.Name)
                .FirstOrDefaultAsync();

            var programName = await _context.Programs
                .Where(p => p.ProgramId == dto.ProgramId)
                .Select(p => p.Name)
                .FirstOrDefaultAsync();

            return new FacultyContactReadDto
            {
                Id = model.Id,
                Description = model.Description,
                Email = model.Email,
                Phone = model.Phone,
                FacultyName = facultyName ?? "N/A",
                ProgramName = programName ?? "N/A"
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (deleted)
                await _repo.SaveAsync();

            return deleted;
        }
    }
}

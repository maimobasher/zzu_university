using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Complaints;
using zzu_university.data.Repository.ComplaintsRepo;
using zzu_university.domain.DTOS.ComplaintsDto;

namespace zzu_university.domain.Service.ComplaintService
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepo _repository;
        private readonly ApplicationDbContext _context;

        public ComplaintService(IComplaintRepo repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task<IEnumerable<ComplaintReadDto>> GetAllAsync()
        {
            var complaints = await _repository.GetAllAsync();
            return complaints.Select(MapToReadDto);
        }

        public async Task<ComplaintReadDto> GetByIdAsync(int id)
        {
            var complaint = await _repository.GetByIdAsync(id);
            return complaint == null ? null : MapToReadDto(complaint);
        }

        public async Task<IEnumerable<ComplaintReadDto>> GetByStudentIdAsync(int studentId)
        {
            var complaints = await _repository.GetByStudentIdAsync(studentId);
            return complaints.Select(MapToReadDto);
        }

        public async Task<ComplaintReadDto> CreateAsync(ComplaintCreateDto dto, int studentId)
        {
            // ✅ تحقق من وجود الطالب
            var studentExists = await _context.Students.AnyAsync(s => s.StudentId == studentId);
            if (!studentExists)
                throw new Exception("Student not found."); // أو يمكنك إرجاع null أو رسالة واضحة

            var complaint = new Complaint
            {
                StudentId = studentId,
                Title = dto.Title,
                Message = dto.Message,
                Email = dto.Email,
                Description = dto.Description,
                Image = dto.Image,
                DateSubmitted = DateTime.Now,
                Status = "Pending"
            };

            var result = await _repository.AddAsync(complaint);
            return MapToReadDto(result);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private ComplaintReadDto MapToReadDto(Complaint complaint)
        {
            return new ComplaintReadDto
            {
                Id = complaint.Id,
                Title = complaint.Title,
                Message = complaint.Message,
                Email = complaint.Email,
                Description = complaint.Description,
                Image = complaint.Image,
                DateSubmitted = complaint.DateSubmitted,
                Status = complaint.Status
            };
        }
    }
}

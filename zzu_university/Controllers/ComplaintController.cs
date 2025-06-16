using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.ComplaintsDto;
using zzu_university.domain.Service.ComplaintService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        // ✅ تسجيل شكوى باستخدام studentId
        // POST: api/Complaint/by-student/12
        [HttpPost("by-student/{studentId}")]
        public async Task<IActionResult> CreateForStudent(
            int studentId,
            [FromBody] ComplaintCreateDto dto)
        {
            try
            {
                var complaint = await _complaintService.CreateAsync(dto, studentId);
                return CreatedAtAction(nameof(GetById), new { id = complaint.Id }, complaint);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ✅ استرجاع شكوى واحدة بالمعرف
        // GET: api/Complaint/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintReadDto>> GetById(int id)
        {
            var complaint = await _complaintService.GetByIdAsync(id);
            if (complaint == null)
                return NotFound();

            return Ok(complaint);
        }

        // ✅ استرجاع كل الشكاوى (مثال لإدارة أو فحص الكل)
        // GET: api/Complaint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintReadDto>>> GetAll()
        {
            var complaints = await _complaintService.GetAllAsync();
            return Ok(complaints);
        }

        // ✅ حذف شكوى
        // DELETE: api/Complaint/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _complaintService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}

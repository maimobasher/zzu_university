using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.Service.StudentService;
using zzu_university.domain.StudentDto;
using zzu_university.domain.DTOS;
using zzu_university.services.Payment;
using Optivem.Framework.Core.Domain;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;

namespace zzu_university.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly FawryPaymentService _fawryPaymentService;
        private readonly ApplicationDbContext _context;

        public StudentController(
          ApplicationDbContext context,
         IStudentService studentService,
          FawryPaymentService fawryPaymentService)
         {
            _context = context;
            _studentService = studentService;
            _fawryPaymentService = fawryPaymentService;
        }


        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // GET: api/student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReadDto>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: api/student
        [HttpPost]
        public async Task<ActionResult<StudentReadDto>> CreateStudent(StudentCreateDto studentCreateDto)
        {
            var student = await _studentService.CreateStudentAsync(studentCreateDto);

            return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
        }

        // PUT: api/student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentUpdateDto studentUpdateDto)
        {
            if (id != studentUpdateDto.StudentId)
            {
                return BadRequest();
            }

            var student = await _studentService.UpdateStudentAsync(studentUpdateDto);

            if (student == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] StudentLoginDto dto)
        {
            // 1. التأكد من وجود اسم المستخدم
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserName == dto.Username);

            if (student == null)
                return NotFound("Username not found.");

            // 2. التحقق من كلمة المرور
            if (student.Password != dto.Password)
                return Unauthorized("Invalid password.");

            // 3. الحصول على أحدث تسجيل للبرنامج
            var latestRegister = await _context.StudentRegisterPrograms
                .Where(r => r.StudentId == student.StudentId)
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync();

            // 4. محاولة الحصول على سجل الدفع بناءً على ProgramId و StudentId
            bool? isPaid = null;
            if (latestRegister != null)
            {
                var payment = await _context.StudentPayments
                    .FirstOrDefaultAsync(p => p.StudentId == student.StudentId && p.ProgramId == latestRegister.ProgramId);

                isPaid = payment?.IsPaid;
            }

            // 5. تجميع الاسم الكامل
            var fullName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim();

            // 6. إرجاع البيانات
            return Ok(new
            {
                student.StudentId,
                StudentName = fullName,
                student.nationalId,
                ProgramCode = latestRegister?.ProgramCode ?? "N/A",
                ProgramAndReferenceCode = latestRegister?.ProgramAndReferenceCode ?? "N/A",
                IsPaid = isPaid ?? false
            });
        }



        // ✅ POST: api/student/5/generate-fawry-code
        [HttpPost("{id}/generate-fawry-code")]
        public async Task<ActionResult<PaymentResponseDto>> GenerateFawryPaymentCode(int id)
        {
            var studentDto = await _studentService.GetStudentByIdAsync(id);

            if (studentDto == null)
            {
                return NotFound("الطالب غير موجود.");
            }

            var paymentResult = await _fawryPaymentService.CreateFawryCodeAsync(studentDto);

            if (paymentResult == null || string.IsNullOrEmpty(paymentResult.ReferenceCode))
            {
                return BadRequest("فشل إنشاء كود فوري.");
            }

            return Ok(paymentResult);
        }
    }
}

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

            // 3. الحصول على أحدث تسجيل للبرنامج مع بيانات البرنامج نفسه
            var latestRegister = await _context.StudentRegisterPrograms
                .Include(r => r.Program) // Include AcadmicProgram
                .Where(r => r.StudentId == student.StudentId)
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync();

            // 4. محاولة الحصول على سجل الدفع بناءً على ProgramId و StudentId
            bool isPaid = false;
            if (latestRegister != null)
            {
                var payment = await _context.StudentPayments
                    .FirstOrDefaultAsync(p => p.StudentId == student.StudentId && p.ProgramId == latestRegister.ProgramId);

                isPaid = payment?.IsPaid ?? false;
            }

            // 5. تجميع الاسم الكامل
            var fullName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim();

            // 6. إرجاع البيانات مع الرسوم الدراسية من برنامج التسجيل
            return Ok(new
            {
                student.StudentId,
                StudentName = fullName,
                student.nationalId,
                student.phone,
                student.email,
                ProgramId = latestRegister?.ProgramId ?? 0,
                ProgramCode = latestRegister?.ProgramCode ?? "N/A",
                ProgramAndReferenceCode = latestRegister?.ProgramAndReferenceCode ?? "N/A",
                IsPaid = isPaid,
                TuitionFees = latestRegister?.Program?.TuitionFees ?? 0,
                Status = latestRegister?.status ?? "N/A"

            });
        }

        [HttpPost("program-status")]
        public async Task<IActionResult> GetProgramStatus([FromBody] StudentProgramQueryDto dto)
        {
            // 1. البحث عن الطالب باستخدام الرقم القومي
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.nationalId == dto.NationalId);

            if (student == null)
                return NotFound("الطالب غير موجود.");

            // 2. إيجاد التسجيل باستخدام StudentId و ProgramAndReferenceCode
            var register = await _context.StudentRegisterPrograms
                .FirstOrDefaultAsync(r =>
                    r.StudentId == student.StudentId &&
                    r.ProgramAndReferenceCode == dto.ProgramAndReferenceCode);

            if (register == null)
                return NotFound("لا يوجد تسجيل بهذا الكود.");

            // 3. جلب البرنامج الأكاديمي المرتبط
            var program = await _context.Programs
                .FirstOrDefaultAsync(p => p.ProgramId == register.ProgramId);

            // 4. جلب آخر عملية دفع
            var payment = await _context.StudentPayments
                .Where(p => p.StudentId == student.StudentId && p.ProgramId == register.ProgramId)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefaultAsync();

            // 5. الاسم الكامل
            var fullName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim();

            // 6. التجميع والإرجاع
            return Ok(new
            {
                student.StudentId,
                StudentName = fullName,
                student.nationalId,
                student.phone,
                student.email,
                ProgramName = program?.Name ?? "N/A",
                FacultyName = student.faculty ?? "N/A", // من جدول الطلاب
                ProgramCode = register.ProgramCode ?? "N/A",
                ProgramAndReferenceCode = register.ProgramAndReferenceCode,
                Status = register.status ?? "غير محدد",
                IsPaid = payment?.IsPaid ?? false,
                PaymentDate = payment != null
                ? payment.PaymentDate.ToString("yyyy-MM-dd")
                : "لم يتم الدفع"
            });
        }

        [HttpGet("program-info/{nationalId}")]
        public async Task<IActionResult> GetProgramPaymentInfo(string nationalId)
        {
            var result = await (
                from s in _context.Students
                where s.nationalId == nationalId
                join r in _context.StudentRegisterPrograms on s.StudentId equals r.StudentId into regJoin
                from reg in regJoin.OrderByDescending(x => x.Id).Take(1).DefaultIfEmpty()

                join p in _context.Programs on reg.ProgramId equals p.ProgramId into progJoin
                from prog in progJoin.DefaultIfEmpty()

                join pay in _context.StudentPayments
                    on new { s.StudentId, ProgramId = reg.ProgramId } equals new { pay.StudentId, pay.ProgramId }
                    into payJoin
                from payment in payJoin.DefaultIfEmpty()

                select new StudentProgramPaymentInfoDto
                {
                    StudentName = s.firstName + " " + (s.middleName ?? "") + " " + s.lastName,
                    ProgramName = prog != null && prog.Name != null ? prog.Name : "ProgramName has no data",
                    ProgramCode = prog != null && prog.ProgramCode != null ? prog.ProgramCode : "ProgramCode has no data",
                    Status = reg.status ?? "Status has no data",
                    IsPaid = payment != null && payment.IsPaid,
                    PaymentDate = payment.PaymentDate,
                    nationalId=s.nationalId,
                    TuitionFees = prog != null ? prog.TuitionFees : 0

                }
            ).FirstOrDefaultAsync();

            if (result == null)
                return NotFound("No data found for this national ID.");

            return Ok(result);
        }


        [HttpPost("CheckUsernameExists")]
        public async Task<IActionResult> CheckUsernameExists([FromBody] CheckUsernameDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName))
                return BadRequest("Username is required.");

            var exists = await _context.Students
                .AnyAsync(s => s.UserName.ToLower() == dto.UserName.ToLower());

            return Ok(new { UsernameExists = exists });
        }
        [HttpGet("GetStudentProgramsWithStatus")]
        public async Task<IActionResult> GetProgramsWithStatus([FromQuery] string national_id)
        {
            // 1. إيجاد الطالب
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.nationalId == national_id);

            if (student == null)
                return NotFound("الطالب غير موجود");

            // 2. استخراج كل البرامج المسجل بها الطالب
            var programs = await _context.StudentRegisterPrograms
                .Where(p => p.StudentId == student.StudentId)
                .Select(p => new StudentProgramStatusDto
                {
                    ProgramId = p.ProgramId,
                    ProgramCode = p.ProgramCode,
                    ProgramAndReferenceCode = p.ProgramAndReferenceCode,
                    Status = p.status,
                    IsPaid = null  // سيتم تعبئتها لاحقاً
                })
                .ToListAsync();

            // 3. ربط كل برنامج بآخر سجل دفع
            foreach (var item in programs)
            {
                var lastPayment = await _context.StudentPayments
                    .Where(pay => pay.StudentId == student.StudentId && pay.ProgramId == item.ProgramId)
                    .OrderByDescending(pay => pay.PaymentDate)
                    .FirstOrDefaultAsync();

                item.IsPaid = lastPayment?.IsPaid;
            }

            return Ok(programs);
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

﻿using Microsoft.AspNetCore.Mvc;
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

            // يتم إرجاع StudentReadDto كامل بما فيه CertificateId
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
        [HttpPost("login-multi")]
        public async Task<IActionResult> LoginWithPrograms([FromBody] StudentLoginDto dto)
        {
            // 1. التأكد من وجود اسم المستخدم
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserName == dto.Username);

            if (student == null)
                return NotFound("Username not found.");

            // 2. التحقق من كلمة المرور
            if (student.Password != dto.Password)
                return Unauthorized("Invalid password.");

            // 3. جلب جميع التسجيلات مع البرنامج
            var registrations = await _context.StudentRegisterPrograms
                .Include(r => r.Program)
                .Where(r => r.StudentId == student.StudentId)
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            // 4. الاسم الكامل
            var fullName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim();

            // 5. بناء القائمة
            var programList = new List<object>();

            foreach (var reg in registrations)
            {
                var latestPayment = await _context.StudentPayments
                    .Where(p => p.StudentId == student.StudentId && p.ProgramId == reg.ProgramId)
                    .OrderByDescending(p => p.PaymentDate)
                    .FirstOrDefaultAsync();

                // ✅ جلب اسم الكلية بناءً على FacultyId في البرنامج
                var facultyName = await _context.Faculties
                    .Where(f => f.FacultyId == reg.Program.FacultyId)
                    .Select(f => f.Name)
                    .FirstOrDefaultAsync() ?? "N/A";

                programList.Add(new
                {
                    reg.ProgramId,
                    ProgramName = reg.Program?.Name ?? "N/A",
                    FacultyName = facultyName, // ✅ من جدول الكليات
                    reg.ProgramCode,
                    reg.ProgramAndReferenceCode,
                    TuitionFees = reg.Program?.TuitionFees ?? 0,
                    Status = reg.status ?? "N/A",
                    IsPaid = latestPayment?.IsPaid ?? false,
                    PaymentDate = latestPayment?.PaymentDate.ToString("yyyy-MM-dd") ?? "لم يتم الدفع",

                    // 🟦 الإضافات المطلوبة:
                    PaymentId = latestPayment?.Id ?? 0,
                    RegistrationId = reg.Id,
                    reg.RegisterDate,
                    IsRequest = latestPayment?.IsRequest ?? false
                });
            }

            // 6. النتيجة النهائية
            return Ok(new
            {
                student.StudentId,
                StudentName = fullName,
                student.nationalId,
                student.phone,
                student.email,
                Programs = programList
            });
        }


        [HttpPost("login-program-ids")]
        public async Task<IActionResult> LoginWithAllPrograms([FromBody] StudentLoginDto dto)
        {
            // 1. التأكد من وجود اسم المستخدم
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserName == dto.Username);

            if (student == null)
                return NotFound("Username not found.");

            // 2. التحقق من كلمة المرور
            if (student.Password != dto.Password)
                return Unauthorized("Invalid password.");

            // 3. الحصول على كل التسجيلات للطالب مع بيانات البرنامج
            var registrations = await _context.StudentRegisterPrograms
                .Include(r => r.Program)
                .Where(r => r.StudentId == student.StudentId)
                .ToListAsync();

            // 4. تجميع الاسم الكامل
            var fullName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim();

            // 5. بناء القائمة بالـ IDs والتفاصيل
            var programDetails = new List<object>();

            foreach (var reg in registrations)
            {
                var payment = await _context.StudentPayments
                    .FirstOrDefaultAsync(p => p.StudentId == student.StudentId && p.ProgramId == reg.ProgramId);

                programDetails.Add(new
                {
                    reg.ProgramId,
                    reg.ProgramCode,
                    reg.ProgramAndReferenceCode,
                    Status = reg.status ?? "N/A",
                    IsPaid = payment?.IsPaid ?? false,
                    TuitionFees = reg.Program?.TuitionFees ?? 0
                });
            }

            // 6. إرجاع البيانات
            return Ok(new
            {
                student.StudentId,
                StudentName = fullName,
                student.nationalId,
                student.phone,
                student.email,
                Programs = programDetails
            });
        }
        [HttpPost("program-status")]
        public async Task<IActionResult> GetProgramStatus([FromBody] StudentProgramQueryDto dto)
        {
            // 1. البحث عن التسجيل المرتبط بالرقم القومي ورقم الطلب
            var register = await _context.StudentRegisterPrograms
                .Include(r => r.Student)
                .FirstOrDefaultAsync(r =>
                    r.ProgramAndReferenceCode == dto.ProgramAndReferenceCode &&
                    r.Student.nationalId == dto.NationalId);

            if (register == null)
                return NotFound("لا يوجد تسجيل مطابق لهذا الرقم القومي ورقم الطلب.");

            var student = register.Student;

            // 2. جلب بيانات البرنامج مع الكلية
            var program = await _context.Programs
                .Include(p => p.Faculty)
                .FirstOrDefaultAsync(p => p.ProgramId == register.ProgramId);

            // 3. جلب أحدث دفعة
            var payment = await _context.StudentPayments
                .Where(p => p.StudentId == student.StudentId && p.ProgramId == register.ProgramId)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefaultAsync();

            // 4. تحديد رسالة الحالة
            // 4. تحديد رسالة الحالة
            var statusMessage = register.status?.ToLower() switch
            {
                "pending" => "الطلب تحت الدراسة",
                "accepted" => "تم القبول مبدئيًا لحين تقديم الأوراق المطلوبة وسداد المصروفات الدراسية",
                "rejected" => "تم رفض طلبك", // ✅ تعديل الرسالة هنا
                _ => "غير محدد"
            };


            // 5. إنشاء النتيجة
            var result = new
            {
                student.StudentId,
                StudentName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim(),
                student.nationalId,
                student.phone,
                student.email,
                ProgramId = register.ProgramId,
                ProgramName = program?.Name ?? "N/A",
                FacultyName = program?.Faculty?.Name ?? "N/A",
                ProgramCode = register.ProgramCode ?? "N/A",
                ProgramAndReferenceCode = register.ProgramAndReferenceCode,
                RegisterDate = register.RegisterDate ?? "غير متوفر", // 🟡 نوعها string
                Status = register.status ?? "غير محدد",
                StatusMessage = statusMessage, // ✅ رسالة مفهومة
                IsPaid = payment?.IsPaid ?? false,
                PaymentDate = payment?.PaymentDate.ToString("yyyy-MM-dd") ?? "لم يتم الدفع",
                ReferenceCode = payment?.ReferenceCode ?? "غير متوفر"
            };

            return Ok(result);
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
        [HttpPost("CheckEmailExists")]
        public async Task<IActionResult> CheckEmailExists([FromBody] CheckEmailDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email is required.");

            var exists = await _context.Students
                .AnyAsync(s => s.email.ToLower() == dto.Email.ToLower());

            return Ok(new { EmailExists = exists });
        }
        [HttpPut("update-doc-url")]
        public async Task<IActionResult> UpdateStudentDocUrl([FromBody] UpdateDocUrlDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _context.Students.FindAsync(dto.StudentId);
            if (student == null)
                return NotFound("الطالب غير موجود.");

            student.doc_url = dto.DocUrl;

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "تم تحديث رابط المستند بنجاح.",
                student.StudentId,
                student.doc_url
            });
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
        [HttpPost("recover-latest")]
        public async Task<IActionResult> RecoverLatestStudentByEmailAndNationalId([FromBody] StudentRecoveryRequestDto dto)
        {
            var student = await _context.Students
                .Where(s => s.nationalId == dto.NationalId && s.email == dto.Email)
                .OrderByDescending(s => s.StudentId) // آخر تسجيل
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound("لم يتم العثور على حساب مطابق.");

            var result = new StudentRecoveryResultDto
            {
                FullName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim(),
                UserName = student.UserName,
                Email = student.email,
                NationalId = student.nationalId,
                Password = student.Password
            };

            return Ok(result);
        }

        [HttpPost("upload-doc")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadStudentDocument([FromForm] FileUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("⚠️ لم يتم إرسال أي ملف.");

            var student = await _context.Students.FindAsync(dto.StudentId);
            if (student == null)
                return NotFound("⚠️ الطالب غير موجود.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(dto.File.FileName);

            // ✅ توليد رقم عشوائي مكون من 4 أرقام
            var randomNumber = new Random().Next(1000, 9999);

            // ✅ اسم الملف = random + studentId
            var fileName = $"شهادة {randomNumber}{dto.StudentId}{student.nationalId}{extension}";
            var path = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            student.doc_url = $"/uploads/{fileName}";
            await _context.SaveChangesAsync();

            return Ok(new
            {
                FilePath = student.doc_url,
                Message = "✅ تم رفع الملف بنجاح."
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

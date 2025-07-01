using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Repository.ProgramRepo;
using zzu_university.data.Repository.StudentRepo;

[ApiController]
[Route("api/[controller]")]
public class StudentReportController : ControllerBase
{
    private readonly IStudentRepo _studentRepo;
    private readonly IProgramRepo _programRepo;
    private readonly StudentPdfReportService _pdfService;

    public StudentReportController(IStudentRepo studentRepo, IProgramRepo programRepo, StudentPdfReportService pdfService)
    {
        _studentRepo = studentRepo;
        _programRepo = programRepo; // ✅ تم حقن الريبو الخاص بالبرامج
        _pdfService = pdfService;
    }

    [HttpGet("pdf/{id}")]
    public async Task<IActionResult> GeneratePdf(int id)
    {
        var student = await _studentRepo.GetStudentWithProgramAndRegistrationsAsync(id);
        if (student == null)
            return NotFound("Student not found.");

        Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "0";

        // حذف programId من هنا
        var pdfBytes = _pdfService.GenerateStudentReport(student);

        return File(pdfBytes, "application/pdf", $"Student_{id}_Report_{DateTime.Now.Ticks}.pdf", enableRangeProcessing: false);
    }

    [HttpGet("student-program-info/{id}")]
    public async Task<IActionResult> GetStudentProgramInfo(int id)
    {
        // جلب الطالب مع تسجيلاته
        var student = await _studentRepo.GetStudentWithProgramAndRegistrationsAsync(id);
        if (student == null)
            return NotFound("Student not found.");

        // استخراج كل التسجيلات الخاصة به
        var programIds = student.ProgramRegistrations
            .Select(r => new
            {
                StudentId = student.StudentId,
                StudentProgramRegisterId = r.Id
            })
            .ToList();

        return Ok(programIds);
    }
    [HttpGet("pdf-by-program")]
    public async Task<IActionResult> GeneratePdfByProgram([FromQuery] int studentId, [FromQuery] int programId)
    {
        // ✅ التسجيل بالبرنامج المطلوب
        var registration = await _studentRepo.GetStudentRegistrationWithProgramAndFacultyAsync(studentId, programId);
        if (registration == null)
            return NotFound("لم يتم العثور على تسجيل للبرنامج.");

        // ✅ تحميل بيانات الطالب
        var student = await _studentRepo.GetStudentByIdAsync(studentId);
        if (student == null)
            return NotFound("الطالب غير موجود.");

        // ✅ اسم الكلية من البرنامج نفسه
        var facultyName = registration.Program?.Faculty?.Name ?? "N/A";

        // ✅ الدفع
        var payment = await _studentRepo.GetPaymentAsync(studentId, programId);

        var reportData = new
        {
            student.StudentId,
            FullName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim(),
            student.nationalId,
            student.phone,
            student.email,
            ProgramName = registration.Program?.Name ?? "N/A",
            FacultyName = facultyName, // ← هي دي الصح
            ProgramCode = registration.ProgramCode,
            ProgramAndReferenceCode = registration.ProgramAndReferenceCode,
            TuitionFees = registration.Program?.TuitionFees ?? 0,
            Status = registration.status ?? "N/A",
            IsPaid = payment?.IsPaid ?? false,
            PaymentDate = payment?.PaymentDate.ToString("yyyy-MM-dd") ?? "لم يتم الدفع"
        };

        var pdfBytes = _pdfService.GenerateStudentProgramReport(student, registration, payment);

        return File(pdfBytes, "application/pdf", $"Student_{studentId}_Program_{programId}_Report.pdf");
    }


}

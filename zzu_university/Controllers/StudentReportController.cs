using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Repository.StudentRepo;

[ApiController]
[Route("api/[controller]")]
public class StudentReportController : ControllerBase
{
    private readonly IStudentRepo _studentRepo;
    private readonly StudentPdfReportService _pdfService;

    public StudentReportController(IStudentRepo studentRepo, StudentPdfReportService pdfService)
    {
        _studentRepo = studentRepo;
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
        var student = await _studentRepo.GetStudentWithSpecificProgramAsync(studentId, programId);
        if (student == null)
            return NotFound("Student not found or not registered in this program.");

        var registration = student.ProgramRegistrations
            .FirstOrDefault(r => r.ProgramId == programId); // ✅ تم التعديل هنا

        if (registration == null)
            return NotFound("Program registration not found.");

        var payment = await _studentRepo.GetPaymentAsync(studentId, registration.ProgramId);

        var facultyName = registration.Program?.Faculty?.Name ?? "N/A";

        var reportData = new
        {
            student.StudentId,
            FullName = $"{student.firstName} {student.middleName ?? ""} {student.lastName}".Trim(),
            student.nationalId,
            student.phone,
            student.email,
            ProgramName = registration.Program?.Name ?? "N/A",
            FacultyName = facultyName,
            ProgramCode = registration.ProgramCode,
            ProgramAndReferenceCode = registration.ProgramAndReferenceCode,
            TuitionFees = registration.Program?.TuitionFees ?? 0,
            Status = registration.status ?? "N/A",
            IsPaid = payment?.IsPaid ?? false,
            PaymentDate = payment?.PaymentDate.ToString("yyyy-MM-dd") ?? "لم يتم الدفع"
        };

        var pdfBytes = _pdfService.GenerateStudentReport(student, programId);

        Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "0";

        return File(pdfBytes, "application/pdf", $"Student_{studentId}_Program_{programId}_Report.pdf");
    }


}

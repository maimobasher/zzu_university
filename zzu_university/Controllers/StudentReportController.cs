using Microsoft.AspNetCore.Mvc;
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
        var pdfBytes = _pdfService.GenerateStudentReport(student);
        return File(pdfBytes, "application/pdf", $"Student_{id}_Report_{DateTime.Now.Ticks}.pdf", enableRangeProcessing: false);

    }
}

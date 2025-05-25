using Microsoft.AspNetCore.Mvc;
using zzu_university.data.Repository.StudentRepo;

[ApiController]
[Route("api/[controller]")]
public class StudentReportController : ControllerBase
{
    //private readonly StudentRepo _studentRepo;
    private readonly StudentPdfReportService _pdfService;

    private readonly IStudentRepo _studentRepo;

    public StudentReportController(IStudentRepo studentRepo, StudentPdfReportService pdfService)
    {
        _studentRepo = studentRepo;
        _pdfService = pdfService;
    }

    [HttpGet("pdf/{id}")]
    public async Task<IActionResult> GeneratePdf(int id)
    {
        var student = await _studentRepo.GetStudentByIdAsync(id);
        if (student == null)
            return NotFound("Student not found.");

        var pdfBytes = _pdfService.GenerateStudentReport(student);
        return File(pdfBytes, "application/pdf", $"Student_{id}_Report.pdf");
    }
}

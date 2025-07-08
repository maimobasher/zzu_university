using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.Payment;
using zzu_university.domain.DTOS.PaymentDto;

[ApiController]
[Route("api/[controller]")]
public class StudentPaymentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentPaymentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/StudentPayment
    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateDto dto)
    {
        var payment = new StudentPayment
        {
            StudentId = dto.StudentId,
            ProgramId = dto.ProgramId,
            ReferenceCode = dto.ReferenceCode,
            IsPaid = dto.IsPaid,
            PaymentDate = DateTime.UtcNow,
            PaidAmount = dto.PaidAmount,
            CreatedDate = DateTime.UtcNow,
            PaymentType = dto.PaymentType,
            IsRequest = dto.IsRequest,
            price = dto.price

        };

        _context.StudentPayments.Add(payment);
        await _context.SaveChangesAsync();

        return Ok(new { Id = payment.Id });
    }

    // PUT: api/StudentPayment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentUpdateDto dto)
    {
        var payment = await _context.StudentPayments.FindAsync(id);

        if (payment == null)
            return NotFound("Payment not found.");

        payment.ReferenceCode = dto.ReferenceCode;
        payment.IsPaid = dto.IsPaid;

        _context.StudentPayments.Update(payment);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Payment updated successfully." });
    }
    [HttpPut("UpdateIsRequest")]
    public async Task<IActionResult> UpdateIsRequest([FromBody] UpdateIsRequestDto dto)
    {
        var payment = await _context.StudentPayments.FirstOrDefaultAsync(p => p.Id == dto.Id);
        if (payment == null)
        {
            return NotFound("Payment not found.");
        }

        payment.IsRequest = dto.IsRequest;
        await _context.SaveChangesAsync();

        return Ok(new { message = "IsRequest updated successfully." });
    }
    [HttpGet("GetLatestPaymentByStudentId/{studentId}")]
    public async Task<IActionResult> GetLatestPaymentByStudentId(int studentId)
    {
        var latestPayment = await _context.StudentPayments
            .Where(p => p.StudentId == studentId)
            .OrderByDescending(p => p.Id) // أو OrderByDescending(p => p.PaymentDate)
            .FirstOrDefaultAsync();

        if (latestPayment == null)
            return NotFound("No payment found for this student.");

        return Ok(latestPayment); // يرجع جميع الأعمدة تلقائيًا
    }

}

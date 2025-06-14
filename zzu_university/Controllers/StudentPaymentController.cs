using Microsoft.AspNetCore.Mvc;
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
            CreatedAt = DateTime.UtcNow
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
}

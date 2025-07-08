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

    // ✅ POST: api/StudentPayment
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
            price = dto.price,
            is_deleted = false
        };

        _context.StudentPayments.Add(payment);
        await _context.SaveChangesAsync();

        return Ok(new { Id = payment.Id });
    }

    // ✅ PUT: api/StudentPayment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentUpdateDto dto)
    {
        var payment = await _context.StudentPayments.FindAsync(id);

        if (payment == null || payment.is_deleted)
            return NotFound("Payment not found or already deleted.");

        payment.ReferenceCode = dto.ReferenceCode;
        payment.IsPaid = dto.IsPaid;

        _context.StudentPayments.Update(payment);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Payment updated successfully." });
    }

    // ✅ تحديث IsRequest فقط
    [HttpPut("UpdateIsRequest")]
    public async Task<IActionResult> UpdateIsRequest([FromBody] UpdateIsRequestDto dto)
    {
        var payment = await _context.StudentPayments.FirstOrDefaultAsync(p => p.Id == dto.Id && !p.is_deleted);
        if (payment == null)
        {
            return NotFound("Payment not found or deleted.");
        }

        payment.IsRequest = dto.IsRequest;
        await _context.SaveChangesAsync();

        return Ok(new { message = "IsRequest updated successfully." });
    }

    // ✅ GET: آخر دفعة للطالب بدون المحذوفة
    [HttpGet("GetLatestPaymentByStudentId/{studentId}")]
    public async Task<IActionResult> GetLatestPaymentByStudentId(int studentId)
    {
        var latestPayment = await _context.StudentPayments
            .Where(p => p.StudentId == studentId && !p.is_deleted)
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync();

        if (latestPayment == null)
            return NotFound("No payment found for this student.");

        return Ok(latestPayment);
    }

    // ✅ Soft Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeletePayment(int id)
    {
        var payment = await _context.StudentPayments.FindAsync(id);

        if (payment == null)
            return NotFound("Payment not found.");

        if (payment.is_deleted)
        {
            // ✅ إنشاء نسخة جديدة إذا كان محذوف
            var newPayment = new StudentPayment
            {
                StudentId = payment.StudentId,
                ProgramId = payment.ProgramId,
                ReferenceCode = payment.ReferenceCode,
                IsPaid = payment.IsPaid,
                PaymentDate = DateTime.UtcNow,
                PaidAmount = payment.PaidAmount,
                CreatedDate = DateTime.UtcNow,
                PaymentType = payment.PaymentType,
                IsRequest = payment.IsRequest,
                price = payment.price,
                is_deleted = false
            };

            _context.StudentPayments.Add(newPayment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "🔁 Payment was previously deleted. A new copy was added.", NewPaymentId = newPayment.Id });
        }

        // ✅ تعديل is_deleted = true
        payment.is_deleted = true;
        await _context.SaveChangesAsync();

        return Ok(new { message = "✅ Payment soft-deleted successfully." });
    }
}

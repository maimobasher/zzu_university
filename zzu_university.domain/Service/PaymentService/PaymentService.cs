using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Payment;
using zzu_university.domain.DTOS.PaymentDto;

namespace zzu_university.domain.Service.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreatePaymentAsync(PaymentCreateDto dto)
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
                is_deleted = dto.is_deleted

            };

            await _context.StudentPayments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<PaymentResultDto?> GetPaymentStatusAsync(int studentId, int programId)
        {
            var payment = await _context.StudentPayments
                .Where(p => p.StudentId == studentId && p.ProgramId == programId)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefaultAsync();

            if (payment == null)
                return null;

            return new PaymentResultDto
            {
                Id = payment.Id,
                StudentId = payment.StudentId,
                ProgramId = payment.ProgramId,
                ReferenceCode = payment.ReferenceCode,
                IsPaid = payment.IsPaid,
                PaymentDate = payment.PaymentDate,
                PaidAmount = payment.PaidAmount,
                CreatedDate = payment.CreatedDate,
                PaymentType = payment.PaymentType,
                IsRequest = (bool)payment.IsRequest,
                price = payment.price,
                is_deleted = payment.is_deleted
            }; 
        }

        public async Task<string> SoftDeletePaymentAsync(int paymentId)
        {
            var payment = await _context.StudentPayments.FindAsync(paymentId);

            if (payment == null)
                return "not_found";

            if (payment.is_deleted)
            {
                // ✅ إنشاء نسخة جديدة
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

                await _context.StudentPayments.AddAsync(newPayment);
                await _context.SaveChangesAsync();
                return "restored";
            }

            // ✅ Soft Delete
            payment.is_deleted = true;
            _context.StudentPayments.Update(payment);
            await _context.SaveChangesAsync();
            return "deleted";
        }

        public async Task<List<PaymentResultDto>> GetAllPaymentsForStudentAsync(int studentId)
        {
            return await _context.StudentPayments
                .Where(p => p.StudentId == studentId)
                .Select(p => new PaymentResultDto
                {
                    Id = p.Id,
                    StudentId = p.StudentId,
                    ProgramId = p.ProgramId,
                    ReferenceCode = p.ReferenceCode,
                    IsPaid = p.IsPaid,
                    PaymentDate = p.PaymentDate ,
                    PaidAmount = p.PaidAmount,
                    CreatedDate = p.CreatedDate,
                    PaymentType = p.PaymentType,
                    IsRequest = (bool)p.IsRequest,
                    price = p.price ,
                    is_deleted = p.is_deleted
                })
                .ToListAsync();
        }
    }

}

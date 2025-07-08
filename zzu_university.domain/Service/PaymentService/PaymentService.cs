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
                price = dto.price

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
                price = payment.price
            }; 
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
                    price = p.price 
                })
                .ToListAsync();
        }
    }

}

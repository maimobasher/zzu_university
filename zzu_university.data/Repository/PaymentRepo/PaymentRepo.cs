using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Data;
using zzu_university.data.Model.Payment;

namespace zzu_university.data.Repository.PaymentRepo
{
    public class StudentPaymentRepository : IStudentPaymentRepo
    {
        private readonly ApplicationDbContext _context;

        public StudentPaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StudentPayment payment)
        {
            await _context.StudentPayments.AddAsync(payment);
        }

        public async Task<StudentPayment?> GetLatestPaymentAsync(int studentId, int programId)
        {
            return await _context.StudentPayments
                .Where(p => p.StudentId == studentId && p.ProgramId == programId)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StudentPayment>> GetPaymentsByStudentAsync(int studentId)
        {
            return await _context.StudentPayments
                .Where(p => p.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<bool> HasPaidAsync(int studentId, int programId)
        {
            return await _context.StudentPayments
                .AnyAsync(p => p.StudentId == studentId && p.ProgramId == programId && p.IsPaid);
        }
    }

}

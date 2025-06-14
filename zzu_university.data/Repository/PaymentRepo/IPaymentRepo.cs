using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Payment;

namespace zzu_university.data.Repository.PaymentRepo
{
    public interface IStudentPaymentRepo
    {
        Task AddAsync(StudentPayment payment);
        Task<StudentPayment?> GetLatestPaymentAsync(int studentId, int programId);
        Task<IEnumerable<StudentPayment>> GetPaymentsByStudentAsync(int studentId);
        Task<bool> HasPaidAsync(int studentId, int programId);
    }

}

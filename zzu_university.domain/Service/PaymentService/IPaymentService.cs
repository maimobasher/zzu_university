using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS.PaymentDto;

namespace zzu_university.domain.Service.PaymentService
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(PaymentCreateDto dto);
        Task<PaymentResultDto?> GetPaymentStatusAsync(int studentId, int programId);
        Task<List<PaymentResultDto>> GetAllPaymentsForStudentAsync(int studentId);
        Task<string> SoftDeletePaymentAsync(int paymentId);
    }

}

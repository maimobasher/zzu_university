using System.Threading.Tasks;
using zzu_university.domain.StudentDto;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.Payment
{
    public interface IFawryPaymentService
    {
        Task<PaymentResponseDto> CreateFawryCodeAsync(StudentReadDto student);
    }
}

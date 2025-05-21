using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using zzu_university.domain.DTOS;
using zzu_university.domain.StudentDto;
using zzu_university.data.Data;
using zzu_university.data.Model.Payment;
using zzu_university.domain.Service.Payment;

namespace zzu_university.services.Payment
{
    public class FawryPaymentService : IFawryPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        private readonly string merchantCode = "YOUR_MERCHANT_CODE";
        private readonly string secureKey = "YOUR_SECURE_KEY";

        public FawryPaymentService(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        private string GenerateSignature(string merchantRefNum, string customerMobile, decimal amount)
        {
            var raw = merchantCode + merchantRefNum + customerMobile + amount.ToString("F2") + secureKey;
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(raw));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public async Task<PaymentResponseDto> CreateFawryCodeAsync(StudentReadDto student)
        {
            var merchantRefNum = $"ZZU-{student.StudentId}-{DateTime.UtcNow.Ticks}";
            var amount = 1500m;

            var signature = GenerateSignature(merchantRefNum, student.phone, amount);

            var requestBody = new
            {
                merchantCode,
                merchantRefNum,
                customerName = $"{student.firstName} {student.lastName}",
                customerMobile = student.phone,
                customerEmail = student.email,
                paymentExpiry = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                amount,
                signature
            };

            var response = await _httpClient.PostAsync(
                "https://www.atfawry.com/ECommerceWeb/Fawry/payments/charge",
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
            {
                return new PaymentResponseDto { Message = "فشل إنشاء كود فوري." };
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var referenceCode = doc.RootElement.GetProperty("referenceNumber").GetString();

            // حفظ في قاعدة البيانات
            var payment = new StudentPayment
            {
                StudentId = student.StudentId,
                ProgramId = student.SelectedProgramId,
                ReferenceCode = referenceCode,
                IsPaid = false
            };

            _context.StudentPayments.Add(payment);
            await _context.SaveChangesAsync();

            return new PaymentResponseDto
            {
                ReferenceCode = referenceCode,
                Message = "تم إنشاء كود فوري بنجاح."
            };
        }
    }
}

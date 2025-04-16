using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.Identity
{
    public class RegisterDto:IDtos
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty; // تأكيد كلمة المرور
        public string Phone { get; set; }
        public string UserType { get; set; }
    }
}

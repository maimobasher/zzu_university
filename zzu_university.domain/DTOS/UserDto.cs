using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS
{
    public class UserDto
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }//admin,user
        public Boolean IsActive { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public object Phone { get; internal set; }
        public object Address { get; internal set; }
        public object UserType { get; internal set; }
    }
}

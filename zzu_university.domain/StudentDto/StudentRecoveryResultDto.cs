using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.StudentDto
{
    public class StudentRecoveryResultDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public string Password { get; set; }
    }

}

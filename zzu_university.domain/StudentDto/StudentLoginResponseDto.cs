using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.StudentDto
{
    public class StudentLoginResponseDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string NationalId { get; set; }
        public string ProgramCode { get; set; }
        public string RegistrationCode { get; set; }
    }
}

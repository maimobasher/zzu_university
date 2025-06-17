using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS
{
    public class StudentRegisterProgramDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProgramId { get; set; }
        public string?  ProgramCode { get; set; }

        public string? RegistrationCode { get; set; }
        public string ?ProgramAndReferenceCode { get; set; }//=> $"{ProgramCode}{RegistrationCode}";
        public string status { get; set; } //=> "Pending" or "Accepted" or "Rejected"
        public string? RegisterDate { get; set; }
    }
}

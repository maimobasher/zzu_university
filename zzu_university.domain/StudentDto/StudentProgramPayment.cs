using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.StudentDto
{
    public class StudentProgramPaymentInfoDto
    {
        public string StudentName { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public string Status { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal TuitionFees { get; set; }
        public string nationalId { get; set; }
    }

}

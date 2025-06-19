using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS
{
    public class StudentProgramStatusDto
    {
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramAndReferenceCode { get; set; }
        public string Status { get; set; }
        public bool? IsPaid { get; set; }  // ممكن يكون null إذا ما فيش عملية دفع
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.ProgramDetails
{
    public class ProgramDetailsReadDto
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string AdmissionRequirements { get; set; }
        public string Bylaw { get; set; }
        public string TuitionFees { get; set; }
        public string Courses { get; set; }
        public string Files { get; set; }
        public string ContactInfo { get; set; }
    }

}

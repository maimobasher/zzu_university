using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.ProgramDto
{
    public class ProgramReadDto
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TuitionFees { get; set; }
        public int DurationInYears { get; set; }
        public int FacultyId { get; set; }
    }
}

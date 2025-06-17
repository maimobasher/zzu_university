using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model.ProgramDetails
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace zzu_university.data.Model.Program
    {
        public class ProgramDetails
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public int ProgramId { get; set; }

            public string AdmissionRequirements { get; set; }
            public string Bylaw { get; set; }
            public string TuitionFees { get; set; }
            public string Courses { get; set; }
            public string Files { get; set; }
            public string ContactInfo { get; set; }

            [ForeignKey("ProgramId")]
            public AcadmicProgram Program { get; set; }
        }
    }

}

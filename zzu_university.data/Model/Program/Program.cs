using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Student;

namespace zzu_university.data.Model.Program
{
    public class AcadmicProgram
    {
        [Key]
        public int programId { get; set; }

        [Required]
        [MaxLength(150)]
        public string name { get; set; }

        [MaxLength(1000)] 
        public string Description { get; set; }

        [Required]
        public decimal TuitionFees { get; set; } // المصاريف الدراسية

        [Required]
        public int DurationInYears { get; set; } // مدة البرنامج بالسنوات

        public ICollection<zzu_university.data.Model.Student.Student> Students { get; set; }
    }
}


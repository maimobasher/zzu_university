using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.ProgramDto
{
    public class ProgramUpdateDto
    {
        [Required]
        public int ProgramId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public decimal TuitionFees { get; set; }

        [Required]
        public int DurationInYears { get; set; }
        public int FacultyId { get; set; }
    }
}


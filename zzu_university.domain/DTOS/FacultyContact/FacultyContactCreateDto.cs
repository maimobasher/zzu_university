using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.FacultyContact
{
    public class FacultyContactCreateDto
    {
        [Required]
        public string Description { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required]
        public int FacultyId { get; set; }

        [Required]
        public int ProgramId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Program;
namespace zzu_university.data.Model.Student
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(14)]
        public string NationalId { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Program")]
        public int SelectedProgramId { get; set; }
        public AcadmicProgram Program { get; set; }

        public bool IsPaymentCompleted { get; set; } = false;
    }
}

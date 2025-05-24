using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.StudentDto
{
    public class StudentUpdateDto
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(150)]
        public string firstName { get; set; }
        [Required]
        [MaxLength(150)]
        public string middleName { get; set; }
        [Required]
        [MaxLength(150)]
        public string lastName { get; set; }

        [Required]
        [MaxLength(14)]
        public string nationalId { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
        public string dateOfBirth { get; set; }
        public int gender { get; set; }
        public string nationality { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string highSchool { get; set; }
        public string graduationYear { get; set; }
        public string gpa { get; set; }
        public string faculty { get; set; }
        public string semester { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string program { get; set; }
        public int SelectedProgramId { get; set; }
        public bool IsPaymentCompleted { get; set; }

    }
}


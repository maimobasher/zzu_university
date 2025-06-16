using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace zzu_university.data.Model.Faculty
{
    public class Faculty
    {
        [Key]
        public int FacultyId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
       //public ICollection<FacultyContact> FacultyContacts { get; set; }
        public ICollection<AcadmicProgram> AcadmicPrograms { get; set; }
    }
}

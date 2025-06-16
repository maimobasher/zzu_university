using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacultyModel = zzu_university.data.Model.Faculty.Faculty;

namespace zzu_university.data.Model.FacultyContact
{
    public class FacultyContact
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Relationships
        public int FacultyId { get; set; }
        public FacultyModel Faculty { get; set; }


        public int ProgramId { get; set; }
        public AcadmicProgram Program { get; set; }
    }

}

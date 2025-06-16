using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.FacultyContact
{
    public class FacultyContactReadDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FacultyName { get; set; }

        public string ProgramName { get; set; }
    }
}

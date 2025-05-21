using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS
{
    public class StudentRegisterProgramDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProgramId { get; set; }

        public string RegistrationCode { get; set; }

        public string RegisterDate { get; set; }
    }
}

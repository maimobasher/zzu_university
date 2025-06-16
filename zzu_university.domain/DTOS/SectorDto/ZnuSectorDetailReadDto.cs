using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.SectorDto
{
    public class ZnuSectorDetailReadDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
    }

}

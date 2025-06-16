using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.SectorDto
{
    public class ZnuSectorDepartmentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Media_Url { get; set; }

        public int SectorId { get; set; }

        public List<ZnuSectorDetailReadDto>? Details { get; set; }
    }

}

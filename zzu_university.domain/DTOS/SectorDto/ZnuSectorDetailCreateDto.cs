using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.SectorDto
{
    public class ZnuSectorDetailCreateDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

}

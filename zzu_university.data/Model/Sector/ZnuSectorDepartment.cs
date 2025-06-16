using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model.Sector
{
    [Table("Znu_Sector_Department")]
    public class ZnuSectorDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Media_Url { get; set; }

        // Foreign key
        public int SectorId { get; set; }

        // Navigation properties
        public ZnuSector Sector { get; set; }
        public ICollection<ZnuSectorDetail> Details { get; set; }
    }

}

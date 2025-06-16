using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model.Sector
{
    [Table("Znu_Sector")]
    public class ZnuSector
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<ZnuSectorDepartment> Departments { get; set; }
    }

}

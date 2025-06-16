using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model.Sector
{
    [Table("Znu_Sector_Details")]
    public class ZnuSectorDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }

        // Foreign key
        public int DepartmentId { get; set; }

        // Navigation property
        public ZnuSectorDepartment Department { get; set; }
    }

}

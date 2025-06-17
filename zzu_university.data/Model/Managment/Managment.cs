using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Managment;

namespace zzu_university.data.Model
{
    [Table("Managments")]
    public class Management
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? PhoneNumber { get; set; }

        [ForeignKey("ManagementType")]
        public int Type { get; set; }

        public ManagementType ManagementType { get; set; }

        public string ImageUrl { get; set; }
        public string? UserId { get; set; }
        public User? Users { get; set; }
    }

}

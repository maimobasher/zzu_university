using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model.Certificate
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string CertificateName { get; set; }

       
        public DateTime IssueDate { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

       
    }
}

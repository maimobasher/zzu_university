using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.CertificateDto
{
    public class CertificateUpdateDto
    {
        [Required]
        public int Id { get; set; }

       
        [Required]
        [StringLength(100)]
        public string CertificateName { get; set; }
        public bool is_deleted { get; set; }
        public DateTime? IssueDate { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.PaymentDto
{
    public class PaymentCreateDto
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int ProgramId { get; set; }

        [Required]
        public string ReferenceCode { get; set; }

        public bool IsPaid { get; set; } = false;
        public decimal PaidAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PaymentType { get; set; }
    }

}

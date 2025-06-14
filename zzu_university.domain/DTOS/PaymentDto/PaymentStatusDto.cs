using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.PaymentDto
{
    public class PaymentStatusDto
    {
        public bool IsPaid { get; set; }

        public string ReferenceCode { get; set; }

        public DateTime? PaidAt { get; set; }
    }

}

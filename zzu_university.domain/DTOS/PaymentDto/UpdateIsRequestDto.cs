using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.PaymentDto
{
    public class UpdateIsRequestDto
    {
        public int Id { get; set; }
        public bool IsRequest { get; set; }
    }
}

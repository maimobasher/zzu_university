﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.PaymentDto
{
    public class PaymentResultDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProgramId { get; set; }

        public string ReferenceCode { get; set; }

        public bool IsPaid { get; set; }
        public bool is_deleted { get; set; } = false;
        public DateTime PaymentDate { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal price { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PaymentType { get; set; }
        public bool IsRequest { get; set; }

    }
}
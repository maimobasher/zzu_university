// File: Models/StudentPayment.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace zzu_university.data.Model.Payment
{
    public class StudentPayment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int ProgramId { get; set; }

        [Required]
        public string ReferenceCode { get; set; }
        public bool is_deleted { get; set; } = false;
        public bool IsPaid { get; set; }
        public bool? IsRequest { get; set; } = false;
        public decimal PaidAmount { get; set; }
        public decimal price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public int PaymentType { get; set; }
    }
}

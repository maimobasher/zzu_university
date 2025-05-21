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

        public bool IsPaid { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using zzu_university.data.Model.Certificate;
using zzu_university.data.Model.Complaints;
using zzu_university.data.Model.StudentRegisterProgram;

public class Student
{
    [Key]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(150)]
    public string firstName { get; set; }

    [Required]
    [MaxLength(150)]
    public string middleName { get; set; }

    [Required]
    [MaxLength(150)]
    public string lastName { get; set; }

    [Required]
    [MaxLength(14)]
    public string nationalId { get; set; }

    [Required]
    [Phone]
    public string phone { get; set; }

    [Required]
    [EmailAddress]
    public string email { get; set; }
    public string? doc_url { get; set; } // URL to the student's document (e.g., national ID, passport, etc.)    
    public string dateOfBirth { get; set; }
    public int gender { get; set; }
    public bool is_review { get; set; }
    public string user_review { get; set; }
    public string date_review { get; set; }
    public string  gpa_equivalant { get; set; }
    public string percent_equivalant { get; set; }
    public string nationality { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    //public string postalCode { get; set; }
    public   string LiscenceType { get; set; }
    public string guardianPhone { get; set; }
    //public string highSchool { get; set; }
    public string percent { get; set; }
    public string graduationYear { get; set; }
    public string gpa { get; set; }
    public string faculty { get; set; }
   // public string semester { get; set; }
    public string program { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int SelectedProgramId { get; set; }

    [ForeignKey("SelectedProgramId")]
    public AcadmicProgram Program { get; set; }

    public ICollection<StudentRegisterProgram> ProgramRegistrations { get; set; } = new List<StudentRegisterProgram>();
    //public ICollection<Certificate> Certificates { get; set; }
    public ICollection<Complaint> complaints { get; set; }
    public int? CertificateId { get; set; }  // Nullable in case not every student has a certificate

    [ForeignKey("CertificateId")]
    public Certificate Certificate { get; set; }
    public bool IsPaymentCompleted { get; set; } = false;
}

﻿using System.ComponentModel.DataAnnotations;
using zzu_university.data.Model.Faculty;
using zzu_university.data.Model.ProgramDetails.zzu_university.data.Model.Program;
using zzu_university.data.Model.StudentRegisterProgram;
using zzu_university.data.Model.StudentRegisterProgram;

public class AcadmicProgram
{
    [Key]
    public int ProgramId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public decimal TuitionFees { get; set; }
    public bool is_deleted { get; set; } = false;
    [Required]
    public int DurationInYears { get; set; }

    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    public string ProgramCode { get; set; }

    // العلاقة مع الطلبة
    public ICollection<Student> Students { get; set; }

    // العلاقة مع جدول التسجيل
    public ICollection<StudentRegisterProgram> StudentRegistrations { get; set; }
    public ProgramDetails ProgramDetails { get; set; }

}

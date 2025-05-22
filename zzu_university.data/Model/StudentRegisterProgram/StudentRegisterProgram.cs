using System.ComponentModel.DataAnnotations;

namespace zzu_university.data.Model.StudentRegisterProgram
{
    public class StudentRegisterProgram
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int ProgramId { get; set; }

        public string RegistrationCode { get; set; }
        public string RegisterDate { get; set; }
        public string  ProgramCode { get; set; }
        public string ProgramAndReferenceCode { get; set; } //=> $"{ProgramCode}{RegistrationCode}";
        public string status { get; set; } //=> "Pending" or "Accepted" or "Rejected"   
        // العلاقات - بدون [ForeignKey] إذا كان اسم الـ FK واضح
        public Student Student { get; set; }
        public AcadmicProgram Program { get; set; }
    }
}

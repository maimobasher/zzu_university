using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model.Complaints
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }

}

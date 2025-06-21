using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.ComplaintsDto
{
    public class ComplaintCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public int StudentId { get; set; }
        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Image { get; set; }  // يمكن أن يكون اسم الملف أو رابط الصورة
    }

}

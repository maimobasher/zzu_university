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
       
        public string ?Title { get; set; }
        public int? StudentId { get; set; }
       
        public string? Message { get; set; }

       
        public string ?Email { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? WhatsAppPhone { get; set; }

        [StringLength(255)]
        public string? Image { get; set; }  // يمكن أن يكون اسم الملف أو رابط الصورة
    }

}

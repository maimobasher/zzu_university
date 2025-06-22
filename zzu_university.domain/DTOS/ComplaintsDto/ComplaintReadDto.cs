using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.ComplaintsDto
{
    public class ComplaintReadDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public string? Phone { get; set; }
        public string? WhatsAppPhone { get; set; }
        public string ? Status { get; set; }
    }

}

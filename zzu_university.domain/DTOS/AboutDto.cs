using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS
{
    public class AboutDto
    {
        public string? Title { get; set; } // Example: "About Zagazig University"
        public string? Description { get; set; } // HTML or plain text content
        public string? Vision { get; set; } // University vision statement
        public string? Mission { get; set; } // University mission statement
        public string? History { get; set; } // Brief history of the university
        public string? ContactEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; } // University location
    }
}

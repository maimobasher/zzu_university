using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS
{
    public class NewsDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        public string? ImageUrl { get; set; }
    }
}

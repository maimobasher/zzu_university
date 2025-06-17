using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.PrivacyDto
{
    public class PrivacyCreateDto
    {
        [MaxLength(200)]
        public string Title { get; set; }

      
        public string Content { get; set; }
    }
}

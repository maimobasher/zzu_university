using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.FAQDto
{
    public class FaqCreateUpdateDto
    {
        [MaxLength(500)]
        public string Question { get; set; }

      
        public string Answer { get; set; }

      
    }

}

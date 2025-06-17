using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.FAQDto
{
    public class FaqReadDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
       
    }

}

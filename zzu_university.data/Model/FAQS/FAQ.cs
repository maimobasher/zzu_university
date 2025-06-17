using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model.FAQS
{
    public class FAQ
    {
        [Key]
        public int Id { get; set; }

       
        public string Question { get; set; }

      
        public string Answer { get; set; }
    }
}
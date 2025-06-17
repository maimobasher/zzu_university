using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model.Privacy
{
    public class Privacy
    {
        [Key]
        public int Id { get; set; }

   
        [MaxLength(200)]
        public string Title { get; set; }

        
        public string Content { get; set; }
    }
}

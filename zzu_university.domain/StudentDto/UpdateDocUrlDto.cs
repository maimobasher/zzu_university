using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.StudentDto
{
    public class UpdateDocUrlDto
    {
        public int StudentId { get; set; }

        [Required]
        public string DocUrl { get; set; }
    }

}

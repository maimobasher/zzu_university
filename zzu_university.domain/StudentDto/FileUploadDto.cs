using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.StudentDto
{
    public class FileUploadDto
    {
        [FromForm]
        public int StudentId { get; set; }

        [FromForm]
        public IFormFile File { get; set; }
    }

}

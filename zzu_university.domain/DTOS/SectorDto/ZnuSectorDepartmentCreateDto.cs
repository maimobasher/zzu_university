﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.SectorDto
{
    public class ZnuSectorDepartmentCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Media_Url { get; set; }

        [Required]
        public int SectorId { get; set; }
    }

}

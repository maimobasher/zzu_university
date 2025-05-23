﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS
{
    public class UserDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? UserType { get; set; } // "Admin", "Customer"
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}

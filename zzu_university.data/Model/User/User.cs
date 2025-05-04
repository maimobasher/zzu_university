using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace zzu_university.data.Model
{
    public class User : IdentityUser
    {
        public int UserId { get; set; } // تقدر تشيله لو مش محتاجه
        public string? FullName { get; set; }

        public string? Role { get; set; } // دي custom role لو عندك نظام مخصص
        public bool IsActive { get; set; } = true;

        public string? Address { get; set; }
        public string? UserType { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Managment>? Managments { get; set; } = new HashSet<Managment>();
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model
{
    public class User: IdentityUser<int>
    {
        public int Id { get; set; }
        public string?  FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }//admin,user
        public Boolean IsActive { get; set; }
       public string Address { get; set; }
        public string UserType { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Managment>? Managments { get; set; } = new HashSet<Managment>();
        //public object Phone { get; set; }
        //public object Address { get; set; }
        //public object UserType { get; set; }
    }
}

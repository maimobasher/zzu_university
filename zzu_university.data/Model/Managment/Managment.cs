using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.data.Model
{
    public class Managment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public int? UserId { get; set; }
        public User? Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.DTOS.CertificateDto
{
    public class CertificateReadDto
    {
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public DateTime ? IssueDate { get; set; }
        public bool is_deleted { get; set; }
        public string ? Description { get; set; }
        // ✅ معلومات الطالب
        public int? StudentId { get; set; }
        public string StudentFullName { get; set; }
    }
}

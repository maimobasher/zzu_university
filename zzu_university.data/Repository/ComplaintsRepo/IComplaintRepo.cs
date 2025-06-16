using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.Complaints;

namespace zzu_university.data.Repository.ComplaintsRepo
{
    public interface IComplaintRepo
    {
        Task<IEnumerable<Complaint>> GetAllAsync();
        Task<Complaint> GetByIdAsync(int id);
        Task<IEnumerable<Complaint>> GetByStudentIdAsync(int studentId);
        Task<Complaint> AddAsync(Complaint complaint);
        Task<Complaint> UpdateAsync(Complaint complaint);
        Task<bool> DeleteAsync(int id);
    }
}

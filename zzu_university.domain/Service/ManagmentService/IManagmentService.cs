using System.Threading.Tasks;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.ManagmentService
{
    public interface IManagmentService
    {
        Task<ManagmentDto> GetManagmentAsync(int id);  // Accept id to get a specific record
        Task<bool> UpdateManagmentAsync(int id, ManagmentDto managmentDto);  // Accept id for updating
        Task<bool> DeleteManagmentAsync(int id);  // Accept id for deleting a record
    }
}

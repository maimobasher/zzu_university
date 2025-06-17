public interface IManagmentService
{
    Task<ManagmentDto> GetManagmentAsync(int id);
    Task<IEnumerable<ManagmentDto>> GetAllManagmentsAsync();
    Task<ManagmentDto> AddManagmentAsync(ManagmentDto managmentDto);
    Task<bool> UpdateManagmentAsync(int id, ManagmentDto managmentDto);
    Task<bool> DeleteManagmentAsync(int id);
}
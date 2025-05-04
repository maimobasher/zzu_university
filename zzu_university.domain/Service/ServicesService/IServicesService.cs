using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.ServicesService
{
    public interface IServicesService
    {
        Task<IEnumerable<ServicesDto>> GetAllServicesAsync();
        Task<ServicesDto?> GetServiceByIdAsync(int id);
        Task<ServicesDto> CreateServiceAsync(ServicesDto serviceDto);
        Task<bool> UpdateServiceAsync(int id, ServicesDto serviceDto);
        Task<bool> DeleteServiceAsync(int id);
    }
}

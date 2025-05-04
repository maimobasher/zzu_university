using zzu_university.domain.DTOS;
using zzu_university.data.Model.Services;
using zzu_university.data.Repository.ServiceRepo;
using System.Threading.Tasks;

namespace zzu_university.domain.Service.ServicesService
{
    public class ServicesService : IServicesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServicesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ServicesDto>> GetAllServicesAsync()
        {
            var services = await _unitOfWork.Service.GetAllAsync();
            return services.Select(s => new ServicesDto
            {
                Name = s.Name,
                Description = s.Description,
                IconUrl = s.IconUrl
            });
        }

        public async Task<ServicesDto?> GetServiceByIdAsync(int id)
        {
            var service = await _unitOfWork.Service.GetByIdAsync(id);
            if (service == null) return null;

            return new ServicesDto
            {
                Name = service.Name,
                Description = service.Description,
                IconUrl = service.IconUrl
            };
        }

        public async Task<ServicesDto> CreateServiceAsync(ServicesDto serviceDto)
        {
            // Convert the DTO to the model
            var service = new data.Model.Services.Service
            {
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                IconUrl = serviceDto.IconUrl
            };

            // Add the service to the database using the UnitOfWork
            await _unitOfWork.Service.AddAsync(service);
            _unitOfWork.Save();

            // Return the created DTO with the data from the model
            return new ServicesDto
            {
                Name = service.Name,
                Description = service.Description,
                IconUrl = service.IconUrl
            };
        }

        public async Task<bool> UpdateServiceAsync(int id, ServicesDto serviceDto)
        {
            var service = await _unitOfWork.Service.GetByIdAsync(id);
            if (service == null)
                return false;

            service.Name = serviceDto.Name;
            service.Description = serviceDto.Description;
            service.IconUrl = serviceDto.IconUrl;

            _unitOfWork.Service.UpdateAsyncById(id,service);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _unitOfWork.Service.GetByIdAsync(id);
            if (service == null)
                return false;

            _unitOfWork.Service.DeleteAsyncById(id);
            _unitOfWork.Save();
            return true;
        }
    }
}

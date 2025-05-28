using zzu_university.data.Model;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.ManagmentService
{
    public class ManagmentService : IManagmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ManagmentDto> GetManagmentAsync(int id)
        {
            var managment = await _unitOfWork.Managment.GetAsyncById(id);
            if (managment == null) return null;

            return new ManagmentDto
            {
                Name = managment.Name,
                Description = managment.Description,
                ContactEmail = managment.ContactEmail,
                PhoneNumber = managment.PhoneNumber,
                Type = managment.Type,
                ImageUrl = managment.ImageUrl
            };
        }

        public async Task<bool> UpdateManagmentAsync(int id, ManagmentDto managmentDto)
        {
            var managment = await _unitOfWork.Managment.GetAsyncById(id);
            if (managment == null) return false;

            managment.Name = managmentDto.Name;
            managment.Description = managmentDto.Description;
            managment.ContactEmail = managmentDto.ContactEmail;
            managment.PhoneNumber = managmentDto.PhoneNumber;
            managment.Type = managmentDto.Type;
            managment.ImageUrl = managmentDto.ImageUrl;

            await _unitOfWork.Managment.UpdateAsyncById(id, managment);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> DeleteManagmentAsync(int id)
        {
            var managment = await _unitOfWork.Managment.GetAsyncById(id);
            if (managment == null) return false;

            await _unitOfWork.Managment.DeleteAsyncById(id);
            _unitOfWork.Save();
            return true;
        }
    }
}

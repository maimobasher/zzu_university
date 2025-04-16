using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model;
using zzu_university.data.Repository.UnitOfWork;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.ManagmentService
{
    public class ManagmentService:IManagmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ManagmentDto> GetManagmentAsync()
        {
            var managment = await _unitOfWork.Managment.GetAsync();
            if (managment == null) return null;

            return new ManagmentDto
            {
                Name = managment.Name,
                Description = managment.Description,
                ContactEmail = managment.ContactEmail,
                PhoneNumber = managment.PhoneNumber
            };
        }

        public async Task UpdateManagmentAsync(ManagmentDto managmentDto)
        {
            var managment = await _unitOfWork.Managment.GetAsync();
            if (managment == null)
            {
                managment = new Managment
                {
                    Name = managmentDto.Name,
                    Description = managmentDto.Description,
                    ContactEmail = managmentDto.ContactEmail,
                    PhoneNumber = managmentDto.PhoneNumber
                };
                await _unitOfWork.Managment.AddAsync(managment);
            }
            else
            {
                managment.Name = managmentDto.Name;
                managment.Description = managmentDto.Description;
                managment.ContactEmail = managmentDto.ContactEmail;
                managment.PhoneNumber = managmentDto.PhoneNumber;
                _unitOfWork.Managment.Update(managment);
            }

            _unitOfWork.Save();
        }
    }
}

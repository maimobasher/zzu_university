using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.About;
using zzu_university.data.Repository.UnitOfWork;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.AboutService
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AboutService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AboutDto> GetAboutAsync()
        {
            var about = await _unitOfWork.About.GetAsync();
            if (about == null) return null;

            return new AboutDto
            {
                Title = about.Title,
                Description = about.Description,
                Vision = about.Vision,
                Mission = about.Mission,
                History = about.History,
                ContactEmail = about.ContactEmail,
                PhoneNumber = about.PhoneNumber,
                Address = about.Address
            };
        }

        public async Task CreateAboutAsync(AboutDto aboutDto)
        {
            var about = new About
            {
                Title = aboutDto.Title,
                Description = aboutDto.Description,
                Vision = aboutDto.Vision,
                Mission = aboutDto.Mission,
                History = aboutDto.History,
                ContactEmail = aboutDto.ContactEmail,
                PhoneNumber = aboutDto.PhoneNumber,
                Address = aboutDto.Address
            };

            await _unitOfWork.About.AddAsync(about);
            _unitOfWork.Save();
        }

        public async Task UpdateAboutAsync(AboutDto aboutDto)
        {
            var about = await _unitOfWork.About.GetAsync();
            if (about == null) return;

            about.Title = aboutDto.Title;
            about.Description = aboutDto.Description;
            about.Vision = aboutDto.Vision;
            about.Mission = aboutDto.Mission;
            about.History = aboutDto.History;
            about.ContactEmail = aboutDto.ContactEmail;
            about.PhoneNumber = aboutDto.PhoneNumber;
            about.Address = aboutDto.Address;

            _unitOfWork.About.Update(about);
            _unitOfWork.Save();
        }

        public async Task DeleteAboutAsync()
        {
            var about = await _unitOfWork.About.GetAsync();
            if (about == null) return;

            _unitOfWork.About.Delete(about);
            _unitOfWork.Save();
        }
    }
}
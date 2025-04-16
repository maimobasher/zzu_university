using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model.MainPage;
using zzu_university.data.Repository.UnitOfWork;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.MainPageService
{
    public class MainPageService : IMainPageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MainPageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MainPage> GetMainPageAsync()
        {
            return await _unitOfWork.MainPage.GetMainPageAsync();
        }

        public async Task<MainPage> CreateMainPageAsync(MainPageDto mainPageDto)
        {
            var mainPage = new MainPage
            {
                Title = mainPageDto.Title,
                Description = mainPageDto.Description,
                ImageUrl = mainPageDto.ImageUrl
            };

            // استخدام الدالة الصحيحة من IMainPageRepo
            await _unitOfWork.MainPage.AddMainPageAsync(mainPage);
            _unitOfWork.Save();
            return mainPage;
        }

        public async Task<bool> UpdateMainPageAsync(MainPageDto mainPageDto)
        {
            var mainPage = await _unitOfWork.MainPage.GetMainPageAsync();
            if (mainPage == null) return false;

            mainPage.Title = mainPageDto.Title;
            mainPage.Description = mainPageDto.Description;
            mainPage.ImageUrl = mainPageDto.ImageUrl;

            _unitOfWork.MainPage.UpdateMainPage(mainPage);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> DeleteMainPageAsync()
        {
            var mainPage = await _unitOfWork.MainPage.GetMainPageAsync();
            if (mainPage == null) return false;

            _unitOfWork.MainPage.DeleteMainPage(mainPage);
            _unitOfWork.Save();
            return true;
        }

        //public Task<MainPage> GetMainPageAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
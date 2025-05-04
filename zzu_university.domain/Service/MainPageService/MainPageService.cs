using zzu_university.data.Model.MainPage;
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

        public async Task<MainPage> GetMainPageByIdAsync(int id)
        {
            return await _unitOfWork.MainPage.GetMainPageAsyncById(id);
        }

        public async Task<MainPage> CreateMainPageAsync(MainPageDto mainPageDto)
        {
            var mainPage = new MainPage
            {
                Title = mainPageDto.Title,
                Description = mainPageDto.Description,
                ImageUrl = mainPageDto.ImageUrl
            };

            await _unitOfWork.MainPage.AddMainPageAsync(mainPage);
            _unitOfWork.Save();
            return mainPage;
        }

        public async Task<bool> UpdateMainPageAsync(MainPageDto mainPageDto)
        {
            var existing = await _unitOfWork.MainPage.GetMainPageAsyncById(mainPageDto.Id);
            if (existing == null) return false;

            existing.Title = mainPageDto.Title;
            existing.Description = mainPageDto.Description;
            existing.ImageUrl = mainPageDto.ImageUrl;

            await _unitOfWork.MainPage.UpdateMainPageById(mainPageDto.Id, existing);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> DeleteMainPageAsync(int id)
        {
            var existing = await _unitOfWork.MainPage.GetMainPageAsyncById(id);
            if (existing == null) return false;

            await _unitOfWork.MainPage.DeleteMainPageById(id);
            _unitOfWork.Save();
            return true;
        }
    }
}

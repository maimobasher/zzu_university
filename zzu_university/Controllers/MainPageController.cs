using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.MainPage;
using zzu_university.data.Repository.UnitOfWork;
using zzu_university.domain.DTOS;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainPageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MainPageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetMainPage()
        {
            var mainPage = await _unitOfWork.MainPage.GetMainPageAsync();
            if (mainPage == null) return NotFound();
            return Ok(mainPage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMainPage([FromBody] MainPageDto mainPageDto)
        {
            if (mainPageDto == null) return BadRequest("Invalid data.");

            var mainPage = new MainPage
            {
                Title = mainPageDto.Title,
                Description = mainPageDto.Description,
                ImageUrl = mainPageDto.ImageUrl
            };

            await _unitOfWork.MainPage.AddMainPageAsync(mainPage);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetMainPage), new { id = mainPage.Id }, mainPage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMainPage([FromBody] MainPageDto mainPageDto)
        {
            if (mainPageDto == null) return BadRequest("Invalid data.");

            var mainPage = await _unitOfWork.MainPage.GetMainPageAsync();
            if (mainPage == null) return NotFound();

            mainPage.Title = mainPageDto.Title;
            mainPage.Description = mainPageDto.Description;
            mainPage.ImageUrl = mainPageDto.ImageUrl;

            _unitOfWork.MainPage.UpdateMainPage(mainPage);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMainPage()
        {
            var mainPage = await _unitOfWork.MainPage.GetMainPageAsync();
            if (mainPage == null) return NotFound();

            _unitOfWork.MainPage.DeleteMainPage(mainPage);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
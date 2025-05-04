using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zzu_university.data.Model.MainPage;
using zzu_university.domain.DTOS;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    [ApiController]
    public class MainPageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MainPageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/MainPage
        [HttpGet]
        public async Task<IActionResult> GetDefaultMainPage()
        {
            var mainPage = await _unitOfWork.MainPage.GetMainPageAsync();
            if (mainPage == null) return NotFound("No main page found.");
            return Ok(mainPage);
        }

        // GET: api/MainPage/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMainPageById(int id)
        {
            var mainPage = await _unitOfWork.MainPage.GetMainPageAsyncById(id);
            if (mainPage == null) return NotFound($"No main page found with ID {id}.");
            return Ok(mainPage);
        }

        // POST: api/MainPage
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

            return CreatedAtAction(nameof(GetMainPageById), new { id = mainPage.Id }, mainPage);
        }

        // PUT: api/MainPage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMainPage(int id, [FromBody] MainPageDto mainPageDto)
        {
            if (mainPageDto == null || id != mainPageDto.Id)
                return BadRequest("Invalid data or ID mismatch.");

            var existing = await _unitOfWork.MainPage.GetMainPageAsyncById(id);
            if (existing == null) return NotFound($"No main page found with ID {id}.");

            var updatedMainPage = new MainPage
            {
                Id = id,
                Title = mainPageDto.Title,
                Description = mainPageDto.Description,
                ImageUrl = mainPageDto.ImageUrl
            };

            await _unitOfWork.MainPage.UpdateMainPageById(id, updatedMainPage);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainPage(int id)
        {
            var existing = await _unitOfWork.MainPage.GetMainPageAsyncById(id);
            if (existing == null) return NotFound($"No main page found with ID {id}.");

            await _unitOfWork.MainPage.DeleteMainPageById(id);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}

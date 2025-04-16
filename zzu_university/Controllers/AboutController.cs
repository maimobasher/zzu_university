using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model.About;
using zzu_university.data.Repository.UnitOfWork;
using zzu_university.domain.DTOS;
using zzu_university.domain.Service.AboutService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAbout()
        {
            var about = await _aboutService.GetAboutAsync();
            if (about == null)
            {
                return NotFound();
            }
            return Ok(about);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout([FromBody] AboutDto aboutDto)
        {
            if (aboutDto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _aboutService.CreateAboutAsync(aboutDto);
            return CreatedAtAction(nameof(GetAbout), new { }, aboutDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout([FromBody] AboutDto aboutDto)
        {
            if (aboutDto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _aboutService.UpdateAboutAsync(aboutDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAbout()
        {
            await _aboutService.DeleteAboutAsync();
            return NoContent();
        }
    }
}
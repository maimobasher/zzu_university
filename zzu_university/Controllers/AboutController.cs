using Microsoft.AspNetCore.Mvc;
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

        // GET: api/About
        [HttpGet]
        public async Task<IActionResult> GetAbout()
        {
            var about = await _aboutService.GetAboutAsync();
            if (about == null)
                return NotFound("No about information found.");

            return Ok(about);
        }

        // GET: api/About/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(int id)
        {
            var about = await _aboutService.GetByIdAsync(id);
            if (about == null)
                return NotFound($"No about found with ID {id}.");

            return Ok(about);
        }

        // POST: api/About
        [HttpPost]
        public async Task<IActionResult> CreateAbout([FromBody] AboutDto aboutDto)
        {
            if (aboutDto == null)
                return BadRequest("Invalid about data.");

            // Check if the model state is valid (includes ContactEmail validation)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _aboutService.CreateAboutAsync(aboutDto);
            return CreatedAtAction(nameof(GetAboutById), new { id = aboutDto.Id }, aboutDto);
        }

        // PUT: api/About/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbout(int id, [FromBody] AboutDto aboutDto)
        {
            if (aboutDto == null || id != aboutDto.Id)
                return BadRequest("Invalid data or ID mismatch.");

            // Check if the model state is valid (includes ContactEmail validation)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _aboutService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"No about found with ID {id}.");

            await _aboutService.UpdateAboutAsync(aboutDto);
            return NoContent();
        }

        // DELETE: api/About/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var existing = await _aboutService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"No about found with ID {id}.");

            await _aboutService.DeleteAboutAsync(id);
            return NoContent();
        }
    }
}

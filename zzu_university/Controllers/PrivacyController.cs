using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.PrivacyDto;
using zzu_university.domain.Service.PrivacyService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivacyController : ControllerBase
    {
        private readonly IPrivacyService _privacyService;

        public PrivacyController(IPrivacyService privacyService)
        {
            _privacyService = privacyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _privacyService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _privacyService.GetByIdAsync(id);
            if (item == null)
                return NotFound("Privacy entry not found.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrivacyCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _privacyService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PrivacyUpdateDto dto)
        {
            if (!ModelState.IsValid || id != dto.Id)
                return BadRequest(ModelState);

            var updated = await _privacyService.UpdateAsync(dto);
            if (updated == null)
                return NotFound("Privacy entry not found.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _privacyService.DeleteAsync(id);
            if (!success)
                return NotFound("Privacy entry not found.");

            return NoContent();
        }
    }
}

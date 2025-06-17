using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.ProgramDetails;
using zzu_university.domain.Service.ProgramDetailsService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramDetailsController : ControllerBase
    {
        private readonly IProgramDetailsService _service;

        public ProgramDetailsController(IProgramDetailsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound("Program details not found.");
            return Ok(item);
        }

        [HttpGet("by-program/{programId}")]
        public async Task<IActionResult> GetByProgramId(int programId)
        {
            var item = await _service.GetByProgramIdAsync(programId);
            if (item == null)
                return NotFound("Program details for this program not found.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramDetailsCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProgramDetailsCreateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound("Program details not found.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound("Program details not found.");

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.FacultyContact;
using zzu_university.domain.Service.FacultyContactService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyContactController : ControllerBase
    {
        private readonly IFacultyContactService _service;

        public FacultyContactController(IFacultyContactService service)
        {
            _service = service;
        }

        // GET: api/FacultyContact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyContactReadDto>>> GetAll()
        {
            var contacts = await _service.GetAllAsync();
            return Ok(contacts);
        }

        // GET: api/FacultyContact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyContactReadDto>> GetById(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null)
                return NotFound("Faculty contact not found.");

            return Ok(contact);
        }

        // POST: api/FacultyContact
        [HttpPost]
        public async Task<ActionResult<FacultyContactReadDto>> Create([FromBody] FacultyContactCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // DELETE: api/FacultyContact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound("Faculty contact not found.");

            return NoContent();
        }
    }
}

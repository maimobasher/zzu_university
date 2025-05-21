using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zzu_university.Services;
using zzu_university.data.DTOs;

namespace zzu_university.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var faculties = await _facultyService.GetAllAsync();
            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var faculty = await _facultyService.GetByIdAsync(id);
            if (faculty == null)
                return NotFound();

            return Ok(faculty);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacultyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _facultyService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.FacultyId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FacultyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _facultyService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _facultyService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

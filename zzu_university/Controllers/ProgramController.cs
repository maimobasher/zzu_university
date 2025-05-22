using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using zzu_university.domain.Service.ProgramService;
using zzu_university.domain.DTOS.ProgramDto;

namespace zzu_university.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        // GET: api/program
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetAllPrograms()
        {
            var programs = await _programService.GetAllProgramsAsync();
            return Ok(programs);
        }

        // GET: api/program/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramReadDto>> GetProgramById(int id)
        {
            var program = await _programService.GetProgramByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }
            return Ok(program);
        }
       


        // POST: api/program
        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramCreateDto programCreateDto)
        {
            await _programService.CreateProgramAsync(programCreateDto);
            return Ok(new { message = "Program created successfully." });
        }

        // PUT: api/program/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] ProgramUpdateDto programUpdateDto)
        {
            if (id != programUpdateDto.ProgramId)
            {
                return BadRequest(new { message = "Program ID mismatch." });
            }

            await _programService.UpdateProgramAsync(programUpdateDto);
            return Ok(new { message = "Program updated successfully." });
        }

        // DELETE: api/program/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            await _programService.DeleteProgramAsync(id);
            return Ok(new { message = "Program deleted successfully." });
        }
        // GET: api/program/faculty/5
        [HttpGet("faculty/{facultyId}")]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetProgramsByFacultyId(int facultyId)
        {
            var programs = await _programService.GetProgramsByFacultyIdAsync(facultyId);
            return Ok(programs);
        }

    }
}

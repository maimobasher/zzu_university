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
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProgram(int id)
        //{
        //    var result = await _programService.SoftDeleteProgramAsync(id);

        //    return result switch
        //    {
        //        "deleted" => Ok(new { message = "✅ Program soft-deleted successfully." }),
        //        "restored" => Ok(new { message = "🔁 Program was previously deleted. A new copy was added." }),
        //        _ => NotFound(new { message = "⛔ Program not found." })
        //    };
        //}

        // DELETE: api/program/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteProgram(int id)
        {
            var result = await _programService.SoftDeleteProgramAsync(id);

            if (result == "restored")
                return Ok(new { message = "Program was deleted previously. A new copy was added." });
            else if (result == "deleted")
                return Ok(new { message = "Program soft-deleted successfully." });
            else
                return NotFound(new { message = "Program not found." });
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

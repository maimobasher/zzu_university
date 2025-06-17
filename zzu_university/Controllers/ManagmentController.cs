using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zzu_university.data.Model;
using zzu_university.domain.DTOS;
using zzu_university.domain.Service.ManagmentService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "Admin")]
    public class ManagmentController : ControllerBase
    {
        private readonly IManagmentService _managmentService;

        public ManagmentController(IManagmentService managmentService)
        {
            _managmentService = managmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _managmentService.GetAllManagmentsAsync();
            if (result == null || !result.Any())
                return NotFound("لا توجد بيانات.");

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _managmentService.GetManagmentAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManagmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _managmentService.AddManagmentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ManagmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _managmentService.UpdateManagmentAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _managmentService.DeleteManagmentAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}

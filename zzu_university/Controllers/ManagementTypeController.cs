using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.ManagementType.zzu_university.domain.DTOS;
using zzu_university.domain.Service.ManagementTypeService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementTypeController : ControllerBase
    {
        private readonly IManagementTypeService _service;

        public ManagementTypeController(IManagementTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            if (result == null || !result.Any())
                return NotFound("لا توجد أنواع إدارة مسجلة.");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound($"لا يوجد نوع إدارة برقم {id}.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManagementTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ManagementTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound($"لا يوجد نوع إدارة برقم {id}.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound($"لا يوجد نوع إدارة برقم {id}.");

            return NoContent();
        }
    }
}

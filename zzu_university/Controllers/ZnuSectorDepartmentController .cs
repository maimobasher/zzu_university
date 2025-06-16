using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.SectorDto;
using zzu_university.domain.Service.ZnuSectorDepartmentService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZnuSectorDepartmentController : ControllerBase
    {
        private readonly IZnuSectorDepartmentService _service;

        public ZnuSectorDepartmentController(IZnuSectorDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var depts = await _service.GetAllAsync();
            return Ok(depts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dept = await _service.GetByIdAsync(id);
            if (dept == null)
                return NotFound("Department not found");
            return Ok(dept);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ZnuSectorDepartmentCreateDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Department added successfully");
        }
    }
}

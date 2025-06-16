using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.SectorDto;
using zzu_university.domain.Service.ZnuSectorDetailService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZnuSectorDetailController : ControllerBase
    {
        private readonly IZnuSectorDetailService _service;

        public ZnuSectorDetailController(IZnuSectorDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = await _service.GetAllAsync();
            return Ok(details);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _service.GetByIdAsync(id);
            if (detail == null)
                return NotFound("Detail not found");
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ZnuSectorDetailCreateDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Detail added successfully");
        }
    }
}

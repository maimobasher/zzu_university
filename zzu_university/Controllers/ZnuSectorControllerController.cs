using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.SectorDto;
using zzu_university.domain.Service.ZnuSectorService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZnuSectorController : ControllerBase
    {
        private readonly IZnuSectorService _service;

        public ZnuSectorController(IZnuSectorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sectors = await _service.GetAllAsync();
            return Ok(sectors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sector = await _service.GetByIdAsync(id);
            if (sector == null)
                return NotFound("Sector not found");
            return Ok(sector);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ZnuSectorReadDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Sector added successfully");
        }
    }
}

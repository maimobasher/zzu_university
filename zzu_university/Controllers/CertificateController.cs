using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.CertificateDto;
using zzu_university.domain.Service.CertificateService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        // GET: api/Certificate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertificateReadDto>>> GetAll()
        {
            var certificates = await _certificateService.GetAllAsync();
            return Ok(certificates);
        }

        // GET: api/Certificate/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificateReadDto>> GetById(int id)
        {
            var certificate = await _certificateService.GetByIdAsync(id);
            if (certificate == null)
                return NotFound();

            return Ok(certificate);
        }

        // GET: api/Certificate/student/{studentId}
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<CertificateReadDto>>> GetByStudentId(int studentId)
        {
            var certificates = await _certificateService.GetByStudentIdAsync(studentId);
            return Ok(certificates);
        }

        // POST: api/Certificate
        [HttpPost]
        public async Task<ActionResult<CertificateReadDto>> Create([FromBody] CertificateCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCertificate = await _certificateService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdCertificate.Id }, createdCertificate);
        }

        // PUT: api/Certificate/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<CertificateReadDto>> Update(int id, [FromBody] CertificateUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var updatedCertificate = await _certificateService.UpdateAsync(dto);
            return Ok(updatedCertificate);
        }

        // DELETE: api/Certificate/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _certificateService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}

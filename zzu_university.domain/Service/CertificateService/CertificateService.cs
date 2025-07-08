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
            var certificates = await _certificateService.GetAllAsync(includeDeleted: false);
            return Ok(certificates);
        }

        // GET: api/Certificate/deleted
        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<CertificateReadDto>>> GetDeleted()
        {
            var deletedCertificates = await _certificateService.GetAllAsync(includeDeleted: true, onlyDeleted: true);
            return Ok(deletedCertificates);
        }

        // GET: api/Certificate/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificateReadDto>> GetById(int id)
        {
            var certificate = await _certificateService.GetByIdAsync(id);
            if (certificate == null || certificate.is_deleted)
                return NotFound();

            return Ok(certificate);
        }

        // POST: api/Certificate
        [HttpPost]
        public async Task<ActionResult<CertificateReadDto>> Create([FromBody] CertificateCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.is_deleted = false; // Ensure it's created as not deleted
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
            if (updatedCertificate == null || updatedCertificate.is_deleted)
                return NotFound();

            return Ok(updatedCertificate);
        }

        // DELETE: api/Certificate/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _certificateService.SoftDeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // PUT: api/Certificate/restore/{id}
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _certificateService.RestoreAsync(id);
            if (!result)
                return NotFound();

            return Ok("✅ تم استرجاع الشهادة بنجاح");
        }

        // DELETE: api/Certificate/hard-delete/{id}
        [HttpDelete("hard-delete/{id}")]
        public async Task<IActionResult> HardDelete(int id)
        {
            var result = await _certificateService.HardDeleteAsync(id);
            if (!result)
                return NotFound();

            return Ok("🗑️ تم حذف الشهادة نهائيًا.");
        }
    }
}

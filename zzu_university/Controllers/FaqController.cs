using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.FAQDto;
using zzu_university.domain.Service.FaqService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        // GET: api/Faq
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FaqReadDto>>> GetAll()
        {
            var faqs = await _faqService.GetAllAsync();
            return Ok(faqs);
        }

        // GET: api/Faq/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FaqReadDto>> GetById(int id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null)
                return NotFound("FAQ not found.");

            return Ok(faq);
        }

        // POST: api/Faq
        [HttpPost]
        public async Task<ActionResult<FaqReadDto>> Create(FaqCreateUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _faqService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Faq/5
        [HttpPut("{id}")]
        public async Task<ActionResult<FaqReadDto>> Update(int id, FaqCreateUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _faqService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound("FAQ not found for update.");

            return Ok(updated);
        }

        // DELETE: api/Faq/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _faqService.DeleteAsync(id);
            if (!deleted)
                return NotFound("FAQ not found for deletion.");

            return NoContent();
        }
    }
}

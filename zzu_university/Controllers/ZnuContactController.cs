using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS.ContactsDto;
using zzu_university.domain.Service.ContactService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZnuContactController : ControllerBase
    {
        private readonly IZnuContactService _contactService;

        public ZnuContactController(IZnuContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/znu-contact
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _contactService.GetAsync();

            if (result == null)
                return NotFound("Contact information not found.");

            return Ok(result);
        }

        // POST: api/znu-contact
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] ZnuContactCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactService.AddOrUpdateAsync(dto);
            return Ok(new { message = "Contact information saved successfully." });
        }
    }
}

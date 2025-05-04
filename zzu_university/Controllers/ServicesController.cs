using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zzu_university.domain.DTOS;
using zzu_university.domain.Service.ServicesService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            IEnumerable<ServicesDto> services = await _servicesService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _servicesService.GetServiceByIdAsync(id);
            if (service == null)
                return NotFound();
            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServicesDto serviceDto)
        {
            if (serviceDto == null)
                return BadRequest("Invalid data.");

            // Create service using the service service
            var createdService = await _servicesService.CreateServiceAsync(serviceDto);

            // Return Created status with the service data
            return CreatedAtAction(nameof(GetServiceById), new { id = createdService.Id }, createdService);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServicesDto serviceDto)
        {
            if (serviceDto == null)
                return BadRequest("Invalid data.");

            var updated = await _servicesService.UpdateServiceAsync(id, serviceDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var deleted = await _servicesService.DeleteServiceAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

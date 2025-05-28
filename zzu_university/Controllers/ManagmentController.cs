using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zzu_university.data.Model;
using zzu_university.domain.DTOS;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    [ApiController]
    public class ManagmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetManagment()
        {
            var managment = await _unitOfWork.Managment.GetAsync();
            if (managment == null) return NotFound();
            return Ok(managment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManagment(int id)
        {
            var managment = await _unitOfWork.Managment.GetAsyncById(id);
            if (managment == null) return NotFound();
            return Ok(managment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManagment([FromBody] ManagmentDto managmentDto)
        {
            if (managmentDto == null) return BadRequest("Invalid data.");

            var managment = new Managment
            {
                Name = managmentDto.Name,
                Description = managmentDto.Description,
                ContactEmail = managmentDto.ContactEmail,
                PhoneNumber = managmentDto.PhoneNumber,
                Type = managmentDto.Type ,
                ImageUrl = managmentDto.ImageUrl
            };

            await _unitOfWork.Managment.AddAsync(managment);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetManagment), new { id = managment.Id }, managment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManagment(int id, [FromBody] ManagmentDto managmentDto)
        {
            if (managmentDto == null) return BadRequest("Invalid data.");

            var existingManagment = await _unitOfWork.Managment.GetAsyncById(id);
            if (existingManagment == null) return NotFound();

            existingManagment.Name = managmentDto.Name;
            existingManagment.Description = managmentDto.Description;
            existingManagment.ContactEmail = managmentDto.ContactEmail;
            existingManagment.PhoneNumber = managmentDto.PhoneNumber;
            existingManagment.Type = managmentDto.Type;
            existingManagment.ImageUrl = managmentDto.ImageUrl; 

            await _unitOfWork.Managment.UpdateAsyncById(id, existingManagment);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManagment(int id)
        {
            var existingManagment = await _unitOfWork.Managment.GetAsyncById(id);
            if (existingManagment == null) return NotFound();

            await _unitOfWork.Managment.DeleteAsyncById(id);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model;
using zzu_university.data.Repository.UnitOfWork;
using zzu_university.domain.DTOS;
using zzu_university.domain.Service.ManagmentService;

namespace zzu_university.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> CreateManagment([FromBody] ManagmentDto managmentDto)
        {
            if (managmentDto == null) return BadRequest("Invalid data.");

            var managment = new Managment
            {
                Name = managmentDto.Name,
                Description = managmentDto.Description,
                ContactEmail = managmentDto.ContactEmail,
                PhoneNumber = managmentDto.PhoneNumber
            };

            await _unitOfWork.Managment.AddAsync(managment);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetManagment), new { id = managment.Id }, managment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManagment([FromBody] ManagmentDto managmentDto)
        {
            if (managmentDto == null) return BadRequest("Invalid data.");

            var managment = await _unitOfWork.Managment.GetAsync();
            if (managment == null) return NotFound();

            managment.Name = managmentDto.Name;
            managment.Description = managmentDto.Description;
            managment.ContactEmail = managmentDto.ContactEmail;
            managment.PhoneNumber = managmentDto.PhoneNumber;

            _unitOfWork.Managment.Update(managment);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteManagment()
        {
            var managment = await _unitOfWork.Managment.GetAsync();
            if (managment == null) return NotFound();

            _unitOfWork.Managment.Delete(managment);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using zzu_university.data.Model.StudentRegisterProgram;
using zzu_university.domain.DTOS;

[Route("api/[controller]")]
[ApiController]
public class StudentRegisterProgramsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentRegisterProgramsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentRegisterProgramDto>>> GetAll()
    {
        var all = await _unitOfWork.StudentRegister.GetAllAsync();

        var result = all.Select(item => new StudentRegisterProgramDto
        {
            Id = item.Id,
            StudentId = item.StudentId,
            ProgramId = item.ProgramId,
            RegistrationCode = item.RegistrationCode,
            RegisterDate = item.RegisterDate,
            ProgramCode = item.Program?.ProgramCode
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentRegisterProgramDto>> GetById(int id)
    {
        var item = await _unitOfWork.StudentRegister.GetByIdAsync(id);
        if (item == null)
            return NotFound();

        var result = new StudentRegisterProgramDto
        {
            Id = item.Id,
            StudentId = item.StudentId,
            ProgramId = item.ProgramId,
            RegistrationCode = item.RegistrationCode,
            RegisterDate = item.RegisterDate,
            ProgramCode = item.Program?.ProgramCode,
            ProgramAndReferenceCode=item.ProgramAndReferenceCode
        };

        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<StudentRegisterProgramDto>> Create(StudentRegisterProgramDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = new StudentRegisterProgram
        {
            StudentId = dto.StudentId,
            ProgramId = dto.ProgramId,
            RegistrationCode = dto.RegistrationCode,
            RegisterDate = dto.RegisterDate,
            ProgramCode = dto.ProgramCode,
            ProgramAndReferenceCode = dto.ProgramCode + "-" + dto.RegistrationCode
        };

        await _unitOfWork.StudentRegister.AddAsync(entity);
        var saved = await _unitOfWork.CompleteAsync();

        if (saved == 0)
            return BadRequest("Failed to save the student register program.");

        dto.Id = entity.Id;
        dto.ProgramAndReferenceCode = entity.ProgramAndReferenceCode; // Include it in the returned DTO

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudentRegisterProgramDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != dto.Id)
            return BadRequest("Id mismatch.");

        var existing = await _unitOfWork.StudentRegister.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.StudentId = dto.StudentId;
        existing.ProgramId = dto.ProgramId;
        existing.RegistrationCode = dto.RegistrationCode;
        existing.RegisterDate = dto.RegisterDate;
        existing.ProgramCode = dto.ProgramCode;
        existing.ProgramAndReferenceCode = dto.ProgramCode + "-" + dto.RegistrationCode;
        _unitOfWork.StudentRegister.Update(existing);
        var saved = await _unitOfWork.CompleteAsync();

        if (saved == 0)
            return BadRequest("Failed to update the student register program.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _unitOfWork.StudentRegister.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        _unitOfWork.StudentRegister.Delete(existing);
        var saved = await _unitOfWork.CompleteAsync();

        if (saved==0)
            return BadRequest("Failed to delete the student register program.");

        return NoContent();
    }
}

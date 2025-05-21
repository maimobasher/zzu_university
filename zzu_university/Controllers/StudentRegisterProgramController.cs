using Microsoft.AspNetCore.Mvc;
using zzu_university.data.Model.StudentRegisterProgram;

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
    public async Task<ActionResult<IEnumerable<StudentRegisterProgram>>> GetAll()
    {
        var all = await _unitOfWork.StudentRegister.GetAllAsync();
        return Ok(all);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentRegisterProgram>> GetById(int id)
    {
        var item = await _unitOfWork.StudentRegister.GetByIdAsync(id);
        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<StudentRegisterProgram>> Create(StudentRegisterProgram model)
    {
        await _unitOfWork.StudentRegister.AddAsync(model);
        var saved = await _unitOfWork.CompleteAsync();

        if (saved==0)
            return BadRequest("Failed to save the student register program.");

        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudentRegisterProgram model)
    {
        if (id != model.Id)
            return BadRequest("Id mismatch.");

        var existing = await _unitOfWork.StudentRegister.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.StudentId = model.StudentId;
        existing.ProgramId = model.ProgramId;
        existing.RegistrationCode = model.RegistrationCode;
        existing.RegisterDate = model.RegisterDate;

        _unitOfWork.StudentRegister.Update(existing);
        var saved = await _unitOfWork.CompleteAsync();

        if (saved==0)
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

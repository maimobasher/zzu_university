using Microsoft.AspNetCore.Mvc;
using zzu_university.data.Model.StudentRegisterProgram;
using zzu_university.domain.DTOS;
using zzu_university.domain.Service.StudentRegisterService;

[Route("api/[controller]")]
[ApiController]
public class StudentRegisterProgramsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentRegisterService _studentRegisterService;
    public StudentRegisterProgramsController(IUnitOfWork unitOfWork,
    IStudentRegisterService studentRegisterService)
    {
        _unitOfWork = unitOfWork;
        _studentRegisterService = studentRegisterService;
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
            ProgramCode = item.Program?.ProgramCode,
            status = item.status
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
            ProgramAndReferenceCode=item.ProgramAndReferenceCode,
            status = item.status
        };

        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<StudentRegisterProgramDto>> Create(StudentRegisterProgramDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var studentExists = await _unitOfWork.Student.ExistsAsync(dto.StudentId);
        if (!studentExists)
            return BadRequest($"Student with Id {dto.StudentId} does not exist.");

        //var programExists = await _unitOfWork.Student.ExistsAsync(dto.ProgramId);
        //if (!programExists)
        //    return BadRequest($"Program with Id {dto.ProgramId} does not exist.");

        // 👇 Call the service to get the next registration code
        dto.RegistrationCode = await _studentRegisterService.GenerateNextRegistrationCodeAsync();

        var entity = new StudentRegisterProgram
        {
            StudentId = dto.StudentId,
            ProgramId = dto.ProgramId,
            RegistrationCode = dto.RegistrationCode,
            RegisterDate = dto.RegisterDate,
            ProgramCode = dto.ProgramCode,
            ProgramAndReferenceCode = dto.ProgramCode + "-" + dto.RegistrationCode,
            status = dto.status
        };

        await _unitOfWork.StudentRegister.AddAsync(entity);
        var saved = await _unitOfWork.CompleteAsync();

        if (saved == 0)
            return BadRequest("Failed to save the student register program.");

        dto.Id = entity.Id;
        dto.ProgramAndReferenceCode = entity.ProgramAndReferenceCode;

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
    }

    [HttpGet("last-registration-code")]
    public async Task<ActionResult<string>> GetNextRegistrationCode()
    {
        var allRecords = await _unitOfWork.StudentRegister.GetAllAsync();

        var lastCode = allRecords
                        .OrderByDescending(x => x.Id)
                        .Select(x => x.RegistrationCode)
                        .FirstOrDefault();

        if (string.IsNullOrEmpty(lastCode))
        {
            return Ok("0001");
        }

        if (!int.TryParse(lastCode, out int lastNumber))
        {
            return BadRequest("Last RegistrationCode is invalid format.");
        }

        int nextNumber = lastNumber + 1;
        string nextCode = nextNumber.ToString("D4");

        return Ok(nextCode);
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
        existing.status = dto.status;
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

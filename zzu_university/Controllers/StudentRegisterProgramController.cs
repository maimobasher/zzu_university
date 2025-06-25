using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Model.StudentRegisterProgram;
using zzu_university.data.Services;
using zzu_university.domain.DTOS;
using zzu_university.domain.Service.StudentRegisterService;

[Route("api/[controller]")]
[ApiController]
public class StudentRegisterProgramsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentRegisterProgramService _studentRegisterProgramService;

    public StudentRegisterProgramsController(
        IUnitOfWork unitOfWork,
        IStudentRegisterProgramService studentRegisterService) // ✅ هذا هو الصح
    {
        _unitOfWork = unitOfWork;
        _studentRegisterProgramService = studentRegisterService;
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
            ProgramCode = item.ProgramCode,
            ProgramAndReferenceCode = string.IsNullOrEmpty(item.ProgramCode)
                ? item.RegistrationCode
                : $"{item.ProgramCode}-{item.RegistrationCode}",
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
            ProgramCode = item.ProgramCode,
            ProgramAndReferenceCode = string.IsNullOrEmpty(item.ProgramCode)
                ? item.RegistrationCode
                : $"{item.ProgramCode}-{item.RegistrationCode}",
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

        // ✅ التحقق من وجود تسجيل مسبق لنفس الطالب على نفس البرنامج
        var isAlreadyRegistered = await _unitOfWork.StudentRegister 
            .AnyAsync(r => r.StudentId == dto.StudentId && r.ProgramId == dto.ProgramId);

        if (isAlreadyRegistered)
            return Conflict("الطالب مسجل بالفعل في هذا البرنامج.");

        // تعيين الكود تلقائيًا
        dto.RegistrationCode = await _studentRegisterProgramService.GenerateNextRegistrationCodeAsync(dto.ProgramId);

        // تعيين التاريخ إذا لم يُرسل
        if (string.IsNullOrWhiteSpace(dto.RegisterDate))
            dto.RegisterDate = DateTime.Now.ToString("yyyy-MM-dd");

        // بناء الكود النهائي
        dto.ProgramAndReferenceCode = string.IsNullOrEmpty(dto.ProgramCode)
            ? dto.RegistrationCode
            : $"{dto.ProgramCode}-{dto.RegistrationCode}";

        var entity = new StudentRegisterProgram
        {
            StudentId = dto.StudentId,
            ProgramId = dto.ProgramId,
            RegistrationCode = dto.RegistrationCode,
            RegisterDate = dto.RegisterDate,
            ProgramCode = dto.ProgramCode,
            ProgramAndReferenceCode = dto.ProgramAndReferenceCode,
            status = dto.status
        };

        await _unitOfWork.StudentRegister.AddAsync(entity);
        var saved = await _unitOfWork.CompleteAsync();

        if (saved == 0)
            return StatusCode(500, "حدث خطأ أثناء حفظ التسجيل.");

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudentRegisterProgramDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != dto.Id)
            return BadRequest("عدم تطابق المعرفات.");

        var existing = await _unitOfWork.StudentRegister.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.StudentId = dto.StudentId;
        existing.ProgramId = dto.ProgramId;
        existing.RegistrationCode = dto.RegistrationCode;
        existing.RegisterDate = dto.RegisterDate;
        existing.ProgramCode = dto.ProgramCode;
        existing.ProgramAndReferenceCode = string.IsNullOrEmpty(dto.ProgramCode)
            ? dto.RegistrationCode
            : $"{dto.ProgramCode}-{dto.RegistrationCode}";
        existing.status = dto.status;

        _unitOfWork.StudentRegister.Update(existing);
        var saved = await _unitOfWork.CompleteAsync();

        if (saved == 0)
            return StatusCode(500, "فشل في تحديث بيانات التسجيل.");

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

        if (saved == 0)
            return StatusCode(500, "فشل في حذف التسجيل.");

        return NoContent();
    }

   



}
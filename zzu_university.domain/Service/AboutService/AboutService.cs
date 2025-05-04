using zzu_university.data.Model.About;
using zzu_university.domain.DTOS;
using zzu_university.domain.Service.AboutService;

public class AboutService : IAboutService
{
    private readonly IUnitOfWork _unitOfWork;

    public AboutService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AboutDto> GetAboutAsync()
    {
        var about = await _unitOfWork.About.GetAsync();
        if (about == null) return null;

        return MapToDto(about);
    }

    public async Task<AboutDto> GetByIdAsync(int id)
    {
        var about = await _unitOfWork.About.GetByIdAsync(id);
        if (about == null) return null;

        return MapToDto(about);
    }

    public async Task CreateAboutAsync(AboutDto aboutDto)
    {
        var about = MapToEntity(aboutDto);
        await _unitOfWork.About.AddAsync(about);
        _unitOfWork.Save();
    }

    public async Task UpdateAboutAsync(AboutDto aboutDto)
    {
        var about = await _unitOfWork.About.GetByIdAsync(aboutDto.Id);
        if (about == null) return;

        about.Title = aboutDto.Title;
        about.Description = aboutDto.Description;
        about.Vision = aboutDto.Vision;
        about.Mission = aboutDto.Mission;
        about.History = aboutDto.History;
        about.ContactEmail = aboutDto.ContactEmail;
        about.PhoneNumber = aboutDto.PhoneNumber;
        about.Address = aboutDto.Address;

        await _unitOfWork.About.UpdateAboutAsync(aboutDto.Id, about);
        _unitOfWork.Save();
    }

    public async Task DeleteAboutAsync(int id)
    {
        var about = await _unitOfWork.About.GetByIdAsync(id);
        if (about == null) return;

        _unitOfWork.About.DeleteAboutAsync(id);
        _unitOfWork.Save();
    }

    // ✅ Map helpers
    private AboutDto MapToDto(About about) => new AboutDto
    {
        Id = about.Id,
        Title = about.Title,
        Description = about.Description,
        Vision = about.Vision,
        Mission = about.Mission,
        History = about.History,
        ContactEmail = about.ContactEmail,
        PhoneNumber = about.PhoneNumber,
        Address = about.Address
    };

    private About MapToEntity(AboutDto dto) => new About
    {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        Vision = dto.Vision,
        Mission = dto.Mission,
        History = dto.History,
        ContactEmail = dto.ContactEmail,
        PhoneNumber = dto.PhoneNumber,
        Address = dto.Address
    };
}

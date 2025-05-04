using zzu_university.domain.DTOS.Identity;

namespace zzu_university.Services
{
    public interface IAuth
    {
        Task<AuthResponsDto> ChangePasswordAsync(ChangePasswordDto dto);
        Task SendPasswordResetCodeAsync(string email);
        Task<AuthResponsDto> ResetPasswordAsync(ResetPassword dto);
        Task<AuthResponsDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponsDto> LoginAsync(LoginDto dto);
        Task<AuthResponsDto> UpdateUserAsync(UpdateUserDto dto);
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using zzu_university.data.Model;
using zzu_university.domain.DTOS.Identity;
using Microsoft.Extensions.Logging;
using zzu_university.data.Data;
using Microsoft.EntityFrameworkCore;
using zzu_university.Servicess;
using System.Net;

namespace zzu_university.Services
{
    public class Auth : IAuth
    {
        private readonly UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Auth> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailService _emailService;

        // Single constructor to inject all dependencies
        public Auth(
            UserManager<User> userManager,
             RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext dbContext,
            ILogger<Auth> logger,
            IEmailService emailService,
            IPasswordHasher<User> passwordHasher)

        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
            _logger = logger;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponsDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration attempt failed: Email {Email} already in use.", dto.Email);
                throw new Exception("Email is already registered.");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                UserType = dto.UserType,
                EmailConfirmed = _configuration.GetValue<bool>("IdentitySettings:ConfirmEmailByDefault")
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("User registration failed: {ErrorMessages}", errorMessages);
                throw new Exception(errorMessages);
            }

            if (!string.IsNullOrWhiteSpace(dto.UserType))
            {
                var normalizedRole = dto.UserType.Trim();
                if (!await _roleManager.RoleExistsAsync(normalizedRole))
                {
                    _logger.LogWarning("Attempt to assign unknown role: {Role}", normalizedRole);
                    throw new Exception($"Role '{normalizedRole}' does not exist.");
                }

                await _userManager.AddToRoleAsync(user, normalizedRole);
            }

            await _emailService.SendEmailAsync(dto.Email, "Welcome to ZZU", "Your account has been created.");

            return GenerateJwt(user);
        }

        public async Task<AuthResponsDto> LoginAsync(LoginDto dto)
        {
            User user = null;

            try
            {
                user = await _dbContext.Users
                    .Where(u => u.Email == dto.Email)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new UnauthorizedAccessException("Invalid credentials.");
                }

                // You can verify the password here if needed (uncomment and use the hashed password logic)
                // if (!_passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password))
                // {
                //     throw new UnauthorizedAccessException("Invalid credentials.");
                // }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request", ex);
            }

            return GenerateJwt(user); // Generate JWT after successful login
        }

        public async Task<AuthResponsDto> UpdateUserAsync(UpdateUserDto dto)
        {
            _logger.LogInformation("Attempting to find user with ID: {UserId}", dto.Id);

            var user = await _userManager.FindByIdAsync(dto.Id); // dto.Id should be string
            if (user == null)
            {
                _logger.LogError("User update failed: User with ID {UserId} not found", dto.Id);
                throw new Exception("User not found.");
            }

            user.FullName = string.IsNullOrWhiteSpace(dto.Name) ? user.FullName : dto.Name;
            user.PhoneNumber = string.IsNullOrWhiteSpace(dto.PhoneNumber) ? user.PhoneNumber : dto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("User update failed: {ErrorMessages}", errorMessages);
                throw new Exception(errorMessages);
            }

            return GenerateJwt(user); // Return updated JWT
        }

        public async Task<AuthResponsDto> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                _logger.LogError("Change password failed: User with email {Email} not found", dto.Email);
                throw new Exception("User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Change password failed: {ErrorMessages}", errorMessages);
                throw new Exception(errorMessages);
            }

            return GenerateJwt(user); // Return new JWT after changing password
        }

        public async Task SendPasswordResetCodeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("Password reset failed: User with email {Email} not found", email);
                return;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            _logger.LogInformation($"Password reset token for {email}: {token}");

            // Create the reset link
            var resetLink = $"https://yourfrontend.com/reset-password?email={email}&token={WebUtility.UrlEncode(token)}";

            var subject = "Password Reset Request";
            var body = $"You requested to reset your password. Click the link below to reset it:\n\n{resetLink}";

            // Send the reset email
            await _emailService.SendEmailAsync(email, subject, body);

            _logger.LogInformation("Password reset email sent to {Email}", email);
        }

        public async Task<AuthResponsDto> ResetPasswordAsync(ResetPassword dto)

        {
            // Log incoming email and token for debugging
            _logger.LogInformation("ResetPasswordAsync called for Email: {Email}", dto.Email);
            _logger.LogInformation("Token received: {Token}", dto.Token);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                _logger.LogError("Password reset failed: User with email {Email} not found", dto.Email);
                throw new Exception("User not found.");
            }

            // Decode the token in case it came URL-encoded
            var decodedToken = WebUtility.UrlDecode(dto.Token);
            _logger.LogInformation("Decoded token: {DecodedToken}", decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, dto.NewPassword);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Password reset failed for {Email}: {Errors}", dto.Email, errorMessages);
                throw new Exception(errorMessages);
            }

            _logger.LogInformation("Password reset successful for {Email}", dto.Email);
            return GenerateJwt(user);
        }

        private AuthResponsDto GenerateJwt(User user)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var duration = int.Parse(_configuration["Jwt:DurationInMinutes"] ?? "60");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var roles = new List<string> { user.UserType };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("FullName", user.FullName ?? ""),
                new Claim("UserType", user.UserType ?? "")
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duration),
                signingCredentials: creds
            );

            return new AuthResponsDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.UserId,
                FullName = user.FullName ?? "",
                Email = user.Email ?? ""
            };
        }
    }
}

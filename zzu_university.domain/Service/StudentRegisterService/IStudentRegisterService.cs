public interface IStudentRegisterService
{
    Task<string> GenerateNextRegistrationCodeAsync();
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using zzu_university.data.Repository;
using zzu_university.data.Repository.AboutRepo;
using zzu_university.data.Repository.CertificateRepo;
using zzu_university.data.Repository.ComplaintsRepo;
using zzu_university.data.Repository.ContactRepo;
using zzu_university.data.Repository.FacultyContactRepo;
using zzu_university.data.Repository.FaqRepo;
using zzu_university.data.Repository.MainPageRepo;
using zzu_university.data.Repository.ManagmentRepo;
using zzu_university.data.Repository.NewsRepo;
using zzu_university.data.Repository.PaymentRepo;
using zzu_university.data.Repository.ProgramRepo;
using zzu_university.data.Repository.ServiceRepo;
using zzu_university.data.Repository.StudentRepo;
using zzu_university.data.Repository.UserRepo;
using zzu_university.data.Repository.ZnuSectorDepartmentRepo;
using zzu_university.data.Repository.ZnuSectorDetailsRepo;
using zzu_university.data.Repository.ZnuSectorRepo;

public interface IUnitOfWork : IDisposable
{
    IAboutRepo About { get; }
    IMainPageRepo MainPage { get; }
    IManagmentRepo Managment { get; }
    INewsRepo News { get; }
    IServiceRepo Service { get; }
    IUserRepo User { get; }
    IStudentRepo Student { get; }
    IProgramRepo Program { get; }
    IFacultyRepo Faculty { get; }
    IStudentRegisterProgramRepo StudentRegister { get; }
    IStudentPaymentRepo StudentPayment { get; }
    ICertificateRepo Certificate { get; }
    IComplaintRepo Complaint { get; }
    IZnuSectorRepo ZnuSector { get; }
    IZnuSectorDepartmentRepo ZnuSectorDepartment { get; }
    IZnuSectorDetailRepo ZnuSectorDetail { get; }
    IZnuContactRepo ZnuContact { get; }
    IFacultyContactRepo FacultyContact { get; }
    IFaqRepo Faq { get; }
    Task SaveAsync();
    Task<int> CompleteAsync();
    int Save(); // ← خليها بحرف S كبير زي الكلاس
}

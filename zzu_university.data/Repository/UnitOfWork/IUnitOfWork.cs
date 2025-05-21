using Microsoft.EntityFrameworkCore;
using zzu_university.data.Repository;
using zzu_university.data.Repository.AboutRepo;
using zzu_university.data.Repository.MainPageRepo;
using zzu_university.data.Repository.ManagmentRepo;
using zzu_university.data.Repository.NewsRepo;
using zzu_university.data.Repository.ProgramRepo;
using zzu_university.data.Repository.ServiceRepo;
using zzu_university.data.Repository.StudentRepo;
using zzu_university.data.Repository.UserRepo;

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
    Task SaveAsync();
    Task CompleteAsync();
    int Save(); // ← خليها بحرف S كبير زي الكلاس
}

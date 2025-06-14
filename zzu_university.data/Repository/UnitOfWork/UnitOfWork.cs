using zzu_university.data.Data;
using zzu_university.data.Repository;
using zzu_university.data.Repository.AboutRepo;
using zzu_university.data.Repository.MainPageRepo;
using zzu_university.data.Repository.ManagmentRepo;
using zzu_university.data.Repository.NewsRepo;
using zzu_university.data.Repository.PaymentRepo;
using zzu_university.data.Repository.ProgramRepo;
using zzu_university.data.Repository.ServiceRepo;
using zzu_university.data.Repository.StudentRepo;
using zzu_university.data.Repository.UserRepo;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IAboutRepo About { get; private set; }
    public IMainPageRepo MainPage { get; private set; }
    public IManagmentRepo Managment { get; private set; }
    public INewsRepo News { get; private set; }
    public IServiceRepo Service { get; private set; }
    public IUserRepo User { get; private set; }
    public IStudentRepo Student { get; private set; }
    public IProgramRepo Program { get; private set; }
    public IFacultyRepo Faculty { get; private set; }
    public IStudentRegisterProgramRepo StudentRegister { get; private set; }
    public IStudentPaymentRepo StudentPayment { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        About = new AboutRepo(_context);
        MainPage = new MainPageRepo(_context);
        Managment = new ManagmentRepo(_context);
        News = new NewsRepo(_context);
        Service = new ServiceRepo(_context);
        Student = new StudentRepo(_context);
        Program = new ProgramRepo(_context);
        Faculty = new FacultyRepository(_context);
        StudentRegister=new StudentRegisterProgramRepository(_context);
        StudentPayment = new StudentPaymentRepository(_context);

    }

    public int Save()
    {
        return _context.SaveChanges();
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}

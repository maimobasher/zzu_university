using Microsoft.EntityFrameworkCore;
using zzu_university.data.Model.About;
using zzu_university.data.Model.MainPage;
using zzu_university.data.Model;
using zzu_university.data.Model.News;
using zzu_university.data.Model.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using zzu_university.data.Model.Payment;
using zzu_university.data.Model.Faculty;
using zzu_university.data.Model.StudentRegisterProgram;

namespace zzu_university.data.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Managment> Managments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<AcadmicProgram> Programs { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<StudentRegisterProgram> StudentRegisterPrograms { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        //public DbSet<User> Users { get; set; }  
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionstring= "Data Source=.;Initial Catalog=zzu_university;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        //    optionsBuilder.UseSqlServer(connectionstring);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // تأكد من أن هذه تأتي مرة واحدة فقط

            // إعداد خصائص About
            modelBuilder.Entity<About>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<About>()
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<About>()
                .Property(a => a.Description)
                .IsRequired();

            modelBuilder.Entity<About>()
                .Property(a => a.Vision)
                .HasMaxLength(1000);

            modelBuilder.Entity<About>()
                .Property(a => a.Mission)
                .HasMaxLength(1000);

            modelBuilder.Entity<About>()
                .Property(a => a.History)
                .HasMaxLength(2000);

            modelBuilder.Entity<About>()
                .Property(a => a.ContactEmail)
                .HasMaxLength(150);

            modelBuilder.Entity<About>()
                .Property(a => a.PhoneNumber)
                .HasMaxLength(20);

            modelBuilder.Entity<About>()
                .Property(a => a.Address)
                .HasMaxLength(500);

            // علاقة Managment مع User
            modelBuilder.Entity<Managment>()
                .HasOne(m => m.Users)
                .WithMany(u => u.Managments)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudentRegisterProgram>()
        .HasOne(srp => srp.Student)
        .WithMany(s => s.ProgramRegistrations)
        .HasForeignKey(srp => srp.StudentId)
        .OnDelete(DeleteBehavior.NoAction);  // هنا منع الحذف التلقائي

            modelBuilder.Entity<StudentRegisterProgram>()
                .HasOne(srp => srp.Program)
                .WithMany(p => p.StudentRegistrations)
                .HasForeignKey(srp => srp.ProgramId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Program)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.SelectedProgramId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Claims;
using zzu_university.data.Data;
using zzu_university.data.Model;
using zzu_university.data.Repository.CertificateRepo;
using zzu_university.data.Repository.ComplaintsRepo;
using zzu_university.data.Repository.ContactRepo;
using zzu_university.data.Repository.FacultyContactRepo;
using zzu_university.data.Repository.FaqRepo;
using zzu_university.data.Repository.ManagementTypeRepo;
using zzu_university.data.Repository.ManagmentRepo;
using zzu_university.data.Repository.PrivacyRepo;
using zzu_university.data.Repository.ProgramDetailsRepo;
using zzu_university.data.Repository.ProgramRepo;
using zzu_university.data.Repository.StudentRepo;
using zzu_university.data.Repository.ZnuSectorDepartmentRepo;
using zzu_university.data.Repository.ZnuSectorDetailsRepo;
using zzu_university.data.Repository.ZnuSectorRepo;
using zzu_university.data.Services;
using zzu_university.domain.Service.AboutService;
using zzu_university.domain.Service.CertificateService;
using zzu_university.domain.Service.ComplaintService;
using zzu_university.domain.Service.ContactService;
using zzu_university.domain.Service.FacultyContactService;
using zzu_university.domain.Service.FaqService;
using zzu_university.domain.Service.MainPageService;
using zzu_university.domain.Service.ManagmentService;
using zzu_university.domain.Service.ManagementTypeService;
using zzu_university.domain.Service.NewsService;
using zzu_university.domain.Service.PaymentService;
using zzu_university.domain.Service.PrivacyService;
using zzu_university.domain.Service.ProgramDetailsService;
using zzu_university.domain.Service.ProgramService;
using zzu_university.domain.Service.ServicesService;
using zzu_university.domain.Service.StudentRegisterService;
using zzu_university.domain.Service.StudentService;
using zzu_university.domain.Service.ZnuSectorDepartmentService;
using zzu_university.domain.Service.ZnuSectorDetailService;
using zzu_university.domain.Service.ZnuSectorService;
using zzu_university.Services;
using zzu_university.Servicess;
using Microsoft.OpenApi.Models;
using zzu_university.domain.Service.ManagementTypeService.zzu_university.domain.Service.ManagementTypeService;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

// 🔹 Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 🔹 Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
    options.AddPolicy("Manager", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User", "Manager"));
});

// 🔹 Dependency Injection
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IMainPageService, MainPageService>();
builder.Services.AddScoped<IManagmentService, ManagmentService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IManagmentRepo, ManagmentRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRegisterProgramService, StudentRegisterProgramService>();
builder.Services.AddScoped<IStudentRegisterService, StudentRegisterService>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<StudentPdfReportService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<ICertificateRepo, CertificateRepo>();
builder.Services.AddScoped<IComplaintRepo, ComplaintRepository>();
builder.Services.AddScoped<IComplaintService, ComplaintService>();
builder.Services.AddScoped<IZnuSectorService, ZnuSectorService>();
builder.Services.AddScoped<IZnuSectorRepo, ZnuSectorRepo>();
builder.Services.AddScoped<IZnuSectorDepartmentRepo, ZnuSectorDepartmentRepo>();
builder.Services.AddScoped<IZnuSectorDepartmentService, ZnuSectorDepartmentService>();
builder.Services.AddScoped<IZnuSectorDetailRepo, ZnuSectorDetailRepo>();
builder.Services.AddScoped<IZnuSectorDetailService, ZnuSectorDetailService>();
builder.Services.AddScoped<IZnuContactRepo, ZnuContactRepo>();
builder.Services.AddScoped<IZnuContactService, ZnuContactService>();
builder.Services.AddScoped<IFacultyContactRepo, FacultyContactRepo>();
builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IFacultyContactService, FacultyContactService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IFaqRepo, FaqRepo>();
builder.Services.AddScoped<IPrivacyService, PrivacyService>();
builder.Services.AddScoped<IPrivacyRepo, PrivacyRepo>();
builder.Services.AddScoped<IProgramDetailsService, ProgramDetailsService>();
builder.Services.AddScoped<IProgramDetailsRepo, ProgramDetailsRepo>();
builder.Services.AddScoped<IManagementTypeRepo, ManagementTypeRepo>();
builder.Services.AddScoped<IManagementTypeService, ManagementTypeService>();
builder.Services.AddScoped<IProgramRepo, ProgramRepo>();
builder.Services.AddLogging();
builder.Services.AddHttpClient<zzu_university.services.Payment.FawryPaymentService>();
builder.Services.AddScoped<zzu_university.services.Payment.FawryPaymentService>();

// 🔹 QuestPDF License
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

// 🔹 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 🔹 Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ZZU University API",
        Version = "v1"
    });

    try
    {
        c.OperationFilter<SwaggerFileOperationFilter>(); // لدعم رفع الملفات IFormFile
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ SwaggerFileOperationFilter Error: {ex.Message}");
    }
});

// 🔹 Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// 🔹 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); // يجب أن يكون هنا قبل أي middlewares تعتمد على الملفات
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

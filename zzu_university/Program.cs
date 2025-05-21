using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using zzu_university.data.Data;
using zzu_university.data.Model;
using zzu_university.domain.Service.AboutService;
using zzu_university.domain.Service.MainPageService;
using zzu_university.domain.Service.ManagmentService;
using zzu_university.domain.Service.NewsService;
using zzu_university.domain.Service.ServicesService;
using zzu_university.Services;
using System.Security.Claims;
using zzu_university.Servicess;
using zzu_university.domain.Service.ProgramService;
using zzu_university.domain.Service.StudentService;
using zzu_university.data.Services;

var builder = WebApplication.CreateBuilder(args);

// Database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Settings
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

// Identity configuration
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

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
       policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("User",
        policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
    options.AddPolicy("Manager",
        policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User", "Manager"));
});

// Custom services
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IMainPageService, MainPageService>();
builder.Services.AddScoped<IManagmentService, ManagmentService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddLogging();  // Ensure logging service is added
builder.Services.AddHttpClient<zzu_university.services.Payment.FawryPaymentService>();
builder.Services.AddScoped<zzu_university.services.Payment.FawryPaymentService>();

// Controllers and Swagger
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS globally
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");  // Apply CORS policy
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

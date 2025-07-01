using System.IO;
using System.Linq;
using zzu_university.data.Data;
using zzu_university.data.Model.Payment;
using zzu_university.data.Model.StudentRegisterProgram;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using QuestPDF.Fluent;
using PdfDocument = QuestPDF.Fluent.Document;

public class StudentPdfReportService
{
    private readonly ApplicationDbContext _context;

    public StudentPdfReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    // 🟩 بدون programId – يعتمد آخر تسجيل
    public byte[] GenerateStudentReport(Student student)
    {
        var registration = student.ProgramRegistrations?
            .OrderByDescending(r => r.Id)
            .FirstOrDefault();

        var latestPayment = registration != null
            ? _context.StudentPayments
                .Where(p => p.StudentId == student.StudentId && p.ProgramId == registration.ProgramId)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefault()
            : null;

        return GenerateReportDocument(student, registration, latestPayment);
    }

    // 🟦 باستخدام programId
    public byte[] GenerateStudentReport(Student student, int programId)
    {
        var registration = student.ProgramRegistrations?
            .FirstOrDefault(r => r.ProgramId == programId);

        var latestPayment = _context.StudentPayments
            .Where(p => p.StudentId == student.StudentId && p.ProgramId == programId)
            .OrderByDescending(p => p.PaymentDate)
            .FirstOrDefault();

        return GenerateReportDocument(student, registration, latestPayment);
    }

    // ✅ جديدة لاستخدامها مباشرة عند توفر البيانات
    public byte[] GenerateStudentProgramReport(Student student, StudentRegisterProgram registration, StudentPayment payment)
    {
        return GenerateReportDocument(student, registration, payment);
    }

    // 🟨 الدالة المشتركة لإنشاء PDF
    private byte[] GenerateReportDocument(Student student, StudentRegisterProgram? registration, StudentPayment? latestPayment)
    {
        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "zagazig_logo.png");

        var document = PdfDocument.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x
                    .FontFamily("Arial")
                    .FontSize(10)
                    .DirectionFromRightToLeft());

                // Header
                page.Header().Row(row =>
                {
                    row.ConstantColumn(80).Height(50).AlignRight().AlignMiddle().Element(e =>
                    {
                        if (File.Exists(logoPath))
                            e.Image(logoPath).FitArea();
                        else
                            e.Text("🔺").FontSize(20);
                    });

                    row.RelativeColumn().AlignCenter().AlignMiddle()
                        .Text("📄 تقرير الطالب").FontSize(16).Bold();
                });

                // Content
                page.Content().Column(col =>
                {
                    void DrawRow(string label, string? value)
                    {
                        col.Item().Row(r =>
                        {
                            r.RelativeColumn()
                             .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                             .Padding(3)
                             .Text(value ?? "—")
                             .AlignRight();

                            r.ConstantColumn(100)
                             .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                             .Padding(3)
                             .Text(label)
                             .FontColor(Colors.Green.Darken2)
                             .SemiBold()
                             .AlignRight();
                        });
                    }

                    // 👤 البيانات الشخصية
                    col.Item().PaddingBottom(3).AlignRight()
                        .Text("👤 البيانات الشخصية").FontSize(12).Bold();

                    DrawRow(":الاسم الكامل", $"{student.firstName} {student.middleName ?? ""} {student.lastName}");
                    DrawRow("البريد الإلكتروني", student.email);
                    DrawRow("رقم الهاتف", student.phone);
                    DrawRow("الرقم القومي", student.nationalId);
                    DrawRow("تاريخ الميلاد", student.dateOfBirth);
                    DrawRow("النوع", student.gender == 1 ? "ذكر" : "أنثى");
                    DrawRow("الجنسية", student.nationality);
                    DrawRow("العنوان", $"{student.address}, {student.city}");
                    DrawRow("نوع الشهادة", student.LiscenceType);
                    DrawRow("رقم تليفون ولى الامر", student.guardianPhone);
                    DrawRow("اسم المستخدم", student.UserName);
                    col.Item().PaddingVertical(5);

                    // 🎓 البيانات التعليمية
                    col.Item().PaddingBottom(3).AlignRight()
                        .Text("🎓 البيانات التعليمية").FontSize(12).Bold();

                    DrawRow("سنة التخرج", student.graduationYear);
                    DrawRow("المعدل التراكمي", student.gpa);
                    DrawRow("النسبة المئوية", student.percent);

                    // ✅ الكلية من البرنامج وليس من student.faculty
                    var facultyName = registration?.Program?.Faculty?.Name ?? "N/A";
                    DrawRow("الكلية", facultyName);
                    col.Item().PaddingVertical(5);

                    // 📘 بيانات البرنامج
                    if (registration?.Program != null)
                    {
                        col.Item().PaddingBottom(3).AlignRight()
                            .Text("📘 البرنامج الأكاديمي").FontSize(12).Bold();

                        DrawRow("اسم البرنامج", registration.Program.Name);
                        col.Item().PaddingVertical(5);
                    }

                    // 📝 بيانات التسجيل
                    if (registration != null)
                    {
                        col.Item().PaddingBottom(3).AlignRight()
                            .Text("📝 بيانات التسجيل").FontSize(12).Bold();

                        DrawRow("تاريخ التسجيل", registration.RegisterDate);
                        DrawRow("كود التقديم", registration.ProgramAndReferenceCode);

                        string statusMessage = registration.status?.ToLower() switch
                        {
                            "pending" => "الطلب تحت الدراسة",
                            "accepted" => "تم القبول مبدئيًا لحين تقديم الأوراق المطلوبة وسداد المصروفات الدراسية",
                            _ => registration.status ?? "—"
                        };

                        DrawRow("الحالة", statusMessage);
                        col.Item().PaddingVertical(5);
                    }

                    // 💳 بيانات الدفع
                    if (latestPayment != null)
                    {
                        col.Item().PaddingBottom(3).AlignRight()
                            .Text("💳 بيانات رسوم التقديم").FontSize(12).Bold();

                        DrawRow("تم الدفع", latestPayment.IsPaid ? "نعم" : "لا");
                        DrawRow("كود المرجع", latestPayment.ReferenceCode);
                        DrawRow("تاريخ الدفع", latestPayment.PaymentDate.ToString("yyyy-MM-dd"));
                    }
                });

                // Footer
                page.Footer().AlignCenter()
                    .Text($"تم الإنشاء بتاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}")
                    .FontSize(9)
                    .SemiBold();
            });
        });

        return document.GeneratePdf();
    }
}

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using zzu_university.data.Data;
using System.IO; // ضروري للمسارات

public class StudentPdfReportService
{
    private readonly ApplicationDbContext _context;

    public StudentPdfReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public byte[] GenerateStudentReport(Student student)
    {
        var latestPayment = _context.StudentPayments
            .Where(p => p.StudentId == student.StudentId)
            .OrderByDescending(p => p.PaymentDate)
            .FirstOrDefault();

        var registration = student.ProgramRegistrations?.FirstOrDefault();

        // ✅ الحصول على مسار اللوجو من wwwroot/images
        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "zagazig_logo.png");

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x
                    .FontFamily("Arial")
                    .FontSize(10)
                    .DirectionFromRightToLeft());

                // ✅ الهيدر مع اللوجو من مسار فعلي
                page.Header().Row(row =>
                {
                    row.ConstantColumn(80).Height(50).AlignRight().AlignMiddle().Element(e =>
                    {
                        if (File.Exists(logoPath))
                            e.Image(logoPath).FitArea();
                        else
                            e.Text("🔺").FontSize(20); // بديل لو الصورة مش موجودة
                    });

                    row.RelativeColumn().AlignCenter().AlignMiddle().Text("📄 تقرير الطالب")
                        .FontSize(16).Bold();
                });

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

                    col.Item().PaddingBottom(3).AlignRight()
                        .Text("👤 البيانات الشخصية").FontSize(12).Bold();

                    DrawRow(":الاسم الكامل", $"{student.firstName} {student.middleName} {student.lastName}");
                    DrawRow("البريد الإلكتروني", student.email);
                    DrawRow("رقم الهاتف", student.phone);
                    DrawRow("الرقم القومي", student.nationalId);
                    DrawRow("تاريخ الميلاد", student.dateOfBirth);
                    DrawRow("النوع", student.gender == 1 ? "ذكر" : "أنثى");
                    DrawRow("الجنسية", student.nationality);
                    DrawRow("العنوان", $"{student.address}, {student.city}");
                    DrawRow("نوع الشهادة", student.LiscenceType);
                    DrawRow("الديانة", student.Religion);
                    DrawRow("اسم المستخدم", student.UserName);
                   // DrawRow("تم الدفع", student.IsPaymentCompleted ? "نعم" : "لا");
                    col.Item().PaddingVertical(5);

                    col.Item().PaddingBottom(3).AlignRight()
                        .Text("🎓 البيانات التعليمية").FontSize(12).Bold();

                    DrawRow("سنة التخرج", student.graduationYear);
                    DrawRow("المعدل التراكمي", student.gpa);
                    DrawRow("النسبة المئوية", student.percent);
                    DrawRow("الكلية", student.faculty);
                    col.Item().PaddingVertical(5);

                    if (student.Program != null)
                    {
                        col.Item().PaddingBottom(3).AlignRight()
                            .Text("📘 البرنامج الأكاديمي").FontSize(12).Bold();

                        DrawRow("اسم البرنامج", student.Program.Name);
                        DrawRow("المدة", $"{student.Program.DurationInYears} سنوات");
                        col.Item().PaddingVertical(5);
                    }

                    if (registration != null)
                    {
                        col.Item().PaddingBottom(3).AlignRight()
                            .Text("📝 بيانات التسجيل").FontSize(12).Bold();

                        DrawRow("تاريخ التسجيل", registration.RegisterDate);
                        DrawRow("كود التقديم", registration.ProgramAndReferenceCode);
                        DrawRow("الحالة", registration.status);
                        col.Item().PaddingVertical(5);
                    }

                    if (latestPayment != null)
                    {
                        col.Item().PaddingBottom(3).AlignRight()
                            .Text("💳 بيانات الدفع").FontSize(12).Bold();

                        DrawRow("تم الدفع", latestPayment.IsPaid ? "نعم" : "لا");
                        DrawRow("كود المرجع", latestPayment.ReferenceCode);
                        DrawRow("تاريخ الدفع", latestPayment.PaymentDate.ToString("yyyy-MM-dd"));
                    }
                });

                page.Footer().AlignCenter()
                    .Text($"تم الإنشاء بتاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}")
                    .FontSize(9)
                    .SemiBold();
            });
        });

        return document.GeneratePdf();
    }
}

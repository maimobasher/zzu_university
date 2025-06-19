using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using zzu_university.data.Data;

public class StudentPdfReportService
{
    private readonly ApplicationDbContext _context;

    public StudentPdfReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public byte[] GenerateStudentReport(Student student)
    {
        // استرجاع آخر عملية دفع
        var latestPayment = _context.StudentPayments
            .Where(p => p.StudentId == student.StudentId)
            .OrderByDescending(p => p.PaymentDate)
            .FirstOrDefault();

        var registration = student.ProgramRegistrations?.FirstOrDefault();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x
                    .FontFamily("Arial")
                    .FontSize(12)
                    .DirectionFromRightToLeft());

                page.Header()
                    .AlignCenter()
                    .Text("📄 تقرير الطالب")
                    .FontSize(20)
                    .Bold();

                page.Content().PaddingVertical(10).Column(col =>
                {
                    void DrawRow(string label, string? value)
                    {
                        col.Item().Row(r =>
                        {
                            r.RelativeColumn()
                             .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                             .Padding(5)
                             .Text(value ?? "—")
                             .AlignRight();

                            r.ConstantColumn(120)
                             .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                             .Padding(5)
                             .Text(label)
                             .FontColor(Colors.Green.Darken2)
                             .SemiBold()
                             .AlignRight();
                        });
                    }

                    // 👤 البيانات الشخصية
                    col.Item().PaddingBottom(5).AlignRight()
                        .Text("👤 البيانات الشخصية").FontSize(14).Bold();

                    DrawRow(":الاسم الكامل", $"{student.firstName} {student.middleName} {student.lastName}");
                    DrawRow("البريد الإلكتروني", student.email);
                    DrawRow("رقم الهاتف", student.phone);
                    DrawRow("الرقم القومي", student.nationalId);
                    DrawRow("تاريخ الميلاد", student.dateOfBirth);
                    DrawRow("النوع", student.gender == 1 ? "ذكر" : "أنثى");
                    DrawRow("الجنسية", student.nationality);
                    DrawRow("العنوان", $"{student.address}, {student.city}");
                    DrawRow("نوع الترخيص", student.LiscenceType);
                    DrawRow("الديانة", student.Religion);

                    col.Item().PaddingVertical(10);

                    // 🎓 البيانات التعليمية
                    col.Item().PaddingBottom(5).AlignRight()
                        .Text("🎓 البيانات التعليمية").FontSize(14).Bold();

                    DrawRow("سنة التخرج", student.graduationYear);
                    DrawRow("المعدل التراكمي", student.gpa);
                    DrawRow("النسبة المئوية", student.percent);
                    DrawRow("الكلية", student.faculty);

                    col.Item().PaddingVertical(10);

                    // 📘 البرنامج الأكاديمي
                    if (student.Program != null)
                    {
                        col.Item().PaddingBottom(5).AlignRight()
                            .Text("📘 البرنامج الأكاديمي").FontSize(14).Bold();

                        DrawRow("اسم البرنامج", student.Program.Name);
                        DrawRow("المدة", $"{student.Program.DurationInYears} سنوات");

                        col.Item().PaddingVertical(10);
                    }

                    // 📝 بيانات التسجيل
                    if (registration != null)
                    {
                        col.Item().PaddingBottom(5).AlignRight()
                            .Text("📝 بيانات التسجيل").FontSize(14).Bold();

                        DrawRow("تاريخ التسجيل", registration.RegisterDate);
                        DrawRow("كود التقديم", registration.ProgramAndReferenceCode);
                        DrawRow("الحالة", registration.status);

                        col.Item().PaddingVertical(10);
                    }

                    // 💳 بيانات الدفع
                    if (latestPayment != null)
                    {
                        col.Item().PaddingBottom(5).AlignRight()
                            .Text("💳 آخر عملية دفع").FontSize(14).Bold();

                        DrawRow("تم الدفع", latestPayment.IsPaid ? "نعم" : "لا");
                        DrawRow("كود المرجع", latestPayment.ReferenceCode);
                        DrawRow("تاريخ الدفع", latestPayment.PaymentDate.ToString("yyyy-MM-dd"));
                    }

                    // 🔐 معلومات النظام
                    col.Item().PaddingBottom(5).AlignRight()
                        .Text("🔐 معلومات النظام").FontSize(14).Bold();

                    DrawRow("اسم المستخدم", student.UserName);
                    DrawRow("تم الدفع", student.IsPaymentCompleted ? "نعم" : "لا");
                });

                page.Footer().AlignCenter()
                    .Text($"تم الإنشاء بتاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}")
                    .FontSize(10)
                    .SemiBold();
            });
        });

        return document.GeneratePdf();
    }
}

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using zzu_university.data.Model.StudentRegisterProgram;

public class StudentPdfReportService
{
    public byte[] GenerateStudentReport(Student student)
    {
        var selectedRegistration = student.ProgramRegistrations?.FirstOrDefault();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                // Apply RTL and Arabic-capable font to everything on this page
                page.DefaultTextStyle(x => x
                    .FontFamily("Arial")
                    .FontSize(12)
                    .DirectionFromRightToLeft());

                page.Margin(50);

                // Header
                page.Header()
                    .AlignCenter()
                    .Text("📄 تقرير الطالب")
                    .FontSize(20)
                    .Bold();

                // Content
                page.Content().PaddingVertical(10).Column(col =>
                {
                    // البيانات الشخصية
                    col.Item().Text("👤 البيانات الشخصية").FontSize(14).Bold();
                    col.Item().Text($"الاسم الكامل: {student.firstName} {student.middleName} {student.lastName}");
                    col.Item().Text($"الرقم القومي: {student.nationalId}");
                    col.Item().Text($"الهاتف: {student.phone}");
                    col.Item().Text($"البريد الإلكتروني: {student.email}");
                    col.Item().Text($"تاريخ الميلاد: {student.dateOfBirth:yyyy-MM-dd}");
                    col.Item().Text($"النوع: {(student.gender == 1 ? "ذكر" : "أنثى")}");
                    col.Item().Text($"الجنسية: {student.nationality}");
                    col.Item().Text($"العنوان: {student.address}, {student.city}, {student.postalCode}");

                    col.Item()
                        .PaddingTop(10)
                        .PaddingBottom(10)
                        .BorderBottom(1)
                        .BorderColor(Colors.Grey.Lighten2);

                    // البيانات التعليمية
                    col.Item().Text("🎓 البيانات التعليمية").FontSize(14).Bold();
                    col.Item().Text($"المدرسة الثانوية: {student.highSchool}");
                    col.Item().Text($"سنة التخرج: {student.graduationYear}");
                    col.Item().Text($"المعدل التراكمي: {student.gpa}");
                    col.Item().Text($"الكلية: {student.faculty}");
                    col.Item().Text($"الفصل الدراسي: {student.semester}");

                    col.Item()
                        .PaddingTop(10)
                        .PaddingBottom(10)
                        .BorderBottom(1)
                        .BorderColor(Colors.Grey.Lighten2);

                    // البرنامج الأكاديمي
                    if (student.Program != null)
                    {
                        col.Item().Text("📘 البرنامج الأكاديمي").FontSize(14).Bold();
                        col.Item().Text($"اسم البرنامج: {student.Program.Name}");
                        col.Item().Text($"كود البرنامج: {student.Program.ProgramCode}");
                        col.Item().Text($"الوصف: {student.Program.Description}");
                        col.Item().Text($"المدة: {student.Program.DurationInYears} سنوات");
                        col.Item().Text($"الرسوم الدراسية: {student.Program.TuitionFees:N2} جنيه");
                    }

                    col.Item()
                        .PaddingTop(10)
                        .PaddingBottom(10)
                        .BorderBottom(1)
                        .BorderColor(Colors.Grey.Lighten2);

                    // بيانات التسجيل
                    if (selectedRegistration != null)
                    {
                        col.Item().Text("📝 بيانات التسجيل").FontSize(14).Bold();
                        col.Item().Text($"كود التسجيل: {selectedRegistration.RegistrationCode}");
                        col.Item().Text($"تاريخ التسجيل: {selectedRegistration.RegisterDate:yyyy-MM-dd}");
                        col.Item().Text($"كود البرنامج: {selectedRegistration.ProgramCode}");
                        col.Item().Text($"كود المرجع: {selectedRegistration.ProgramAndReferenceCode}");
                        col.Item().Text($"الحالة: {selectedRegistration.status}");
                    }

                    col.Item()
                        .PaddingTop(10)
                        .PaddingBottom(10)
                        .BorderBottom(1)
                        .BorderColor(Colors.Grey.Lighten2);

                    // معلومات النظام
                    col.Item().Text("🔐 معلومات النظام").FontSize(14).Bold();
                    col.Item().Text($"اسم المستخدم: {student.UserName}");
                    col.Item().Text($"تم الدفع: {(student.IsPaymentCompleted ? "نعم" : "لا")}");
                });

                // Footer
                page.Footer()
                    .AlignCenter()
                    .Text($"تم الإنشاء بتاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}");
            });
        });

        return document.GeneratePdf();
    }
}

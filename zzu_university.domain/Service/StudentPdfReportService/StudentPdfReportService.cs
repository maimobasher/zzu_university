using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using zzu_university.data.Model.StudentRegisterProgram;

public class StudentPdfReportService
{
    public byte[] GenerateStudentReport(Student student)
    {
        var registration = student.ProgramRegistrations?.FirstOrDefault();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
               
                // 1) RTL + Arabic font
                page.DefaultTextStyle(x => x
                    .FontFamily("Arial")
                    .FontSize(12)
                    .DirectionFromRightToLeft());

                page.Margin(40);

                // 2) Header
                page.Header()
                    .AlignCenter()
                    .Text("📄 تقرير الطالب")
                    .FontSize(20)
                    .Bold();

                page.Content().PaddingVertical(10).Column(col =>
                {
                    // Helper to draw a two‐column row
                    void DrawRow(string label, string? value)
                    {
                        col.Item().Row(r =>
                        {
                            // Value cell
                            r.RelativeColumn()
                             .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                             .Padding(5)
                             .Text(value ?? "—")
                             .AlignRight();

                            // Label cell
                            r.ConstantColumn(120)
                             .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                             .Padding(5)
                             .Text(label)
                             .FontColor(Colors.Green.Darken2)
                             .SemiBold()
                             .AlignRight();
                        });
                    }

                    // ── Personal Information ──
                    col.Item()
                        .PaddingBottom(5)
                        .AlignRight()
                        .Text("👤 البيانات الشخصية")
                        .FontSize(14)
                        .Bold();

                    DrawRow(":الاسم الكامل", $"{student.firstName} {student.middleName} {student.lastName}");
                    DrawRow("البريد الإلكتروني", student.email);
                    DrawRow("رقم الهاتف", student.phone);
                    DrawRow("الرقم القومي", student.nationalId);
                    DrawRow("تاريخ الميلاد", student.dateOfBirth);
                    DrawRow("النوع", student.gender == 1 ? "ذكر" : "أنثى");
                    DrawRow("الجنسية", student.nationality);
                    DrawRow("العنوان", $"{student.address}, {student.city}");

                    col.Item().PaddingVertical(10); // spacing

                    // ── Educational Details ──
                    col.Item()
                        .PaddingBottom(5)
                        .AlignRight()
                        .Text("🎓 البيانات التعليمية")
                        .FontSize(14)
                        .Bold();

                    //DrawRow("المدرسة الثانوية", student.highSchool);
                    DrawRow("سنة التخرج", student.graduationYear);
                    DrawRow("المعدل التراكمي", student.gpa);
                    DrawRow("النسبة المئوية", student.percent);
                    DrawRow("الكلية", student.faculty);
                   // DrawRow("الفصل الدراسي", student.semester);

                    col.Item().PaddingVertical(10);

                    // ── Academic Program ──
                    if (student.Program != null)
                    {
                        col.Item()
                            .PaddingBottom(5)
                            .AlignRight()
                            .Text("📘 البرنامج الأكاديمي")
                            .FontSize(14)
                            .Bold();

                        DrawRow("اسم البرنامج", student.Program.Name);
                        //DrawRow("كود البرنامج", student.Program.ProgramCode);
                       // DrawRow("الوصف", student.Program.Description);
                        DrawRow("المدة", $"{student.Program.DurationInYears} سنوات");
                        //DrawRow("الرسوم الدراسية", $"{student.Program.TuitionFees:N2} جنيه");

                        col.Item().PaddingVertical(10);
                    }

                    // ── Registration Details ──
                    if (registration != null)
                    {
                        col.Item()
                            .PaddingBottom(5)
                            .AlignRight()
                            .Text("📝 بيانات التسجيل")
                            .FontSize(14)
                            .Bold();

                        //DrawRow("كود التسجيل", registration.RegistrationCode);
                        DrawRow("تاريخ التسجيل", registration.RegisterDate);
                        DrawRow("كود التقديم", registration.ProgramAndReferenceCode);
                        DrawRow("الحالة", registration.status);

                        col.Item().PaddingVertical(10);
                    }

                    // ── System Info ──
                    col.Item()
                        .PaddingBottom(5)
                        .AlignRight()
                        .Text("🔐 معلومات النظام")
                        .FontSize(14)
                        .Bold();

                    DrawRow("اسم المستخدم", student.UserName);
                    DrawRow("تم الدفع", student.IsPaymentCompleted ? "نعم" : "لا");
                });

                // 3) Footer
                page.Footer()
                    .AlignCenter()
                    .Text($"تم الإنشاء بتاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}")
                    .FontSize(10)
                    .SemiBold();
            });
        });

        return document.GeneratePdf();
    }
}


using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class StudentPdfReportService
{
    public byte[] GenerateStudentReport(Student student)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Text("Student Report").FontSize(20).Bold().AlignCenter();

                page.Content().PaddingVertical(10).Column(col =>
                {
                    col.Item().Text($"Name: {student.firstName} {student.middleName} {student.lastName}");
                    col.Item().Text($"National ID: {student.nationalId}");
                    col.Item().Text($"Phone: {student.phone}");
                    col.Item().Text($"Email: {student.email}");
                    col.Item().Text($"Date of Birth: {student.dateOfBirth}");
                    col.Item().Text($"Gender: {(student.gender == 1 ? "Male" : "Female")}");
                    col.Item().Text($"Nationality: {student.nationality}");
                    col.Item().Text($"Address: {student.address}, {student.city}, {student.postalCode}");
                    col.Item().Text($"High School: {student.highSchool}");
                    col.Item().Text($"Graduation Year: {student.graduationYear}");
                    col.Item().Text($"GPA: {student.gpa}");
                    col.Item().Text($"Faculty: {student.faculty}");
                    col.Item().Text($"Semester: {student.semester}");
                    col.Item().Text($"Program: {student.Program} (ID: {student.SelectedProgramId})");
                    col.Item().Text($"Username: {student.UserName}");
                });

                page.Footer().AlignCenter().Text($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm}");
            });
        });

        return document.GeneratePdf();
    }
}

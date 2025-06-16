using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zzu_university.data.Migrations
{
    /// <inheritdoc />
    public partial class updateall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "highSchool",
                table: "Students",
                newName: "percent");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "StudentPayments",
                newName: "PaymentDate");

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CertificateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complaints_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyContacts_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyContacts_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Znu_Sector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Znu_Sector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZnuContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZnuContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Znu_Sector_Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Media_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Znu_Sector_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Znu_Sector_Department_Znu_Sector_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Znu_Sector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Znu_Sector_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Znu_Sector_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Znu_Sector_Details_Znu_Sector_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Znu_Sector_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_StudentId",
                table: "Certificates",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_StudentId",
                table: "Complaints",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyContacts_FacultyId",
                table: "FacultyContacts",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyContacts_ProgramId",
                table: "FacultyContacts",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Znu_Sector_Department_SectorId",
                table: "Znu_Sector_Department",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Znu_Sector_Details_DepartmentId",
                table: "Znu_Sector_Details",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "FacultyContacts");

            migrationBuilder.DropTable(
                name: "Znu_Sector_Details");

            migrationBuilder.DropTable(
                name: "ZnuContacts");

            migrationBuilder.DropTable(
                name: "Znu_Sector_Department");

            migrationBuilder.DropTable(
                name: "Znu_Sector");

            migrationBuilder.RenameColumn(
                name: "percent",
                table: "Students",
                newName: "highSchool");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "StudentPayments",
                newName: "CreatedAt");
        }
    }
}

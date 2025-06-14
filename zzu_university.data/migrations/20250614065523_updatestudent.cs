using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zzu_university.data.Migrations
{
    /// <inheritdoc />
    public partial class updatestudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "semester",
                table: "Students",
                newName: "Religion");

            migrationBuilder.RenameColumn(
                name: "postalCode",
                table: "Students",
                newName: "LiscenceType");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Managments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Managments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Managments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Managments");

            migrationBuilder.RenameColumn(
                name: "Religion",
                table: "Students",
                newName: "semester");

            migrationBuilder.RenameColumn(
                name: "LiscenceType",
                table: "Students",
                newName: "postalCode");
        }
    }
}

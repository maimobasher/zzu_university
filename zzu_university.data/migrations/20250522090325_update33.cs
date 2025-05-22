using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zzu_university.data.Migrations
{
    /// <inheritdoc />
    public partial class update33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgramAndReferenceCode",
                table: "StudentRegisterPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramAndReferenceCode",
                table: "StudentRegisterPrograms");
        }
    }
}

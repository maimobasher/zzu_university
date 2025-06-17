using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zzu_university.data.Migrations
{
    /// <inheritdoc />
    public partial class updateinall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managments_ManagementTypes_Type",
                table: "Managments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagementTypes",
                table: "ManagementTypes");

            migrationBuilder.RenameTable(
                name: "ManagementTypes",
                newName: "ManagementType");

            migrationBuilder.AlterColumn<string>(
                name: "ProgramCode",
                table: "StudentRegisterPrograms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagementType",
                table: "ManagementType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managments_ManagementType_Type",
                table: "Managments",
                column: "Type",
                principalTable: "ManagementType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managments_ManagementType_Type",
                table: "Managments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagementType",
                table: "ManagementType");

            migrationBuilder.RenameTable(
                name: "ManagementType",
                newName: "ManagementTypes");

            migrationBuilder.AlterColumn<string>(
                name: "ProgramCode",
                table: "StudentRegisterPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagementTypes",
                table: "ManagementTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managments_ManagementTypes_Type",
                table: "Managments",
                column: "Type",
                principalTable: "ManagementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

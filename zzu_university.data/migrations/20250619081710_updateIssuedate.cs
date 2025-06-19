using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zzu_university.data.Migrations
{
    /// <inheritdoc />
    public partial class updateIssuedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "StudentPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRequest",
                table: "StudentPayments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "StudentPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "StudentPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "StudentPayments");

            migrationBuilder.DropColumn(
                name: "IsRequest",
                table: "StudentPayments");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "StudentPayments");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "StudentPayments");
        }
    }
}

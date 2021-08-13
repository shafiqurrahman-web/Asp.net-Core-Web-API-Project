using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provenance.DataLayer.Migrations
{
    public partial class userData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "User",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "User",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Firstname", "Lastname", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("6ae7b75e-c101-4287-aca5-38aff787fd9c"), "Mohammadreza.tarkhan@gmail.com", "mohammadreza", "Tarkhan", "Mohammadreza Tarkhan", null, new Guid("7cf9e80e-0ffc-4bd3-aa72-4ec80743545d") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("6ae7b75e-c101-4287-aca5-38aff787fd9c"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "User",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "User",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);
        }
    }
}

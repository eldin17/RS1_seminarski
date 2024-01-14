using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class JWTispravke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "KorisnickiNalozi");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "KorisnickiNalozi",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "KorisnickiNalozi",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "KorisnickiNalozi");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "KorisnickiNalozi");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "KorisnickiNalozi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

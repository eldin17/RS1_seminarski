using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class VezaKorisnickiDonator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogId",
                table: "Donatori",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Donatori_KorisnickiNalogId",
                table: "Donatori",
                column: "KorisnickiNalogId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donatori_KorisnickiNalozi_KorisnickiNalogId",
                table: "Donatori",
                column: "KorisnickiNalogId",
                principalTable: "KorisnickiNalozi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donatori_KorisnickiNalozi_KorisnickiNalogId",
                table: "Donatori");

            migrationBuilder.DropIndex(
                name: "IX_Donatori_KorisnickiNalogId",
                table: "Donatori");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogId",
                table: "Donatori");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class VezaKontaktDonator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonatorId",
                table: "Kontakti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kontakti_DonatorId",
                table: "Kontakti",
                column: "DonatorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kontakti_Donatori_DonatorId",
                table: "Kontakti",
                column: "DonatorId",
                principalTable: "Donatori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kontakti_Donatori_DonatorId",
                table: "Kontakti");

            migrationBuilder.DropIndex(
                name: "IX_Kontakti_DonatorId",
                table: "Kontakti");

            migrationBuilder.DropColumn(
                name: "DonatorId",
                table: "Kontakti");
        }
    }
}

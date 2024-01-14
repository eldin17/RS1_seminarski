using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class VezaDonacijaDonator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonatorId",
                table: "Donacije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Donacije_DonatorId",
                table: "Donacije",
                column: "DonatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donacije_Donatori_DonatorId",
                table: "Donacije",
                column: "DonatorId",
                principalTable: "Donatori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donacije_Donatori_DonatorId",
                table: "Donacije");

            migrationBuilder.DropIndex(
                name: "IX_Donacije_DonatorId",
                table: "Donacije");

            migrationBuilder.DropColumn(
                name: "DonatorId",
                table: "Donacije");
        }
    }
}

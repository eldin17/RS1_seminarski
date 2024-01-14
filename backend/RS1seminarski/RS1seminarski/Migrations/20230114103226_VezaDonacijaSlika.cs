using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class VezaDonacijaSlika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonacijaId",
                table: "Slike",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slike_DonacijaId",
                table: "Slike",
                column: "DonacijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slike_Donacije_DonacijaId",
                table: "Slike",
                column: "DonacijaId",
                principalTable: "Donacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slike_Donacije_DonacijaId",
                table: "Slike");

            migrationBuilder.DropIndex(
                name: "IX_Slike_DonacijaId",
                table: "Slike");

            migrationBuilder.DropColumn(
                name: "DonacijaId",
                table: "Slike");
        }
    }
}

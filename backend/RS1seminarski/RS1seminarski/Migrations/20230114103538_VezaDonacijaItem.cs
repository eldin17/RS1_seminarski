using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class VezaDonacijaItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonacijaId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_DonacijaId",
                table: "Items",
                column: "DonacijaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Donacije_DonacijaId",
                table: "Items",
                column: "DonacijaId",
                principalTable: "Donacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Donacije_DonacijaId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_DonacijaId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DonacijaId",
                table: "Items");
        }
    }
}

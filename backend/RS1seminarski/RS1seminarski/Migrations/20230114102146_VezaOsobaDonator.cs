using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class VezaOsobaDonator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OsobaId",
                table: "Donatori",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Donatori_OsobaId",
                table: "Donatori",
                column: "OsobaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donatori_Osobe_OsobaId",
                table: "Donatori",
                column: "OsobaId",
                principalTable: "Osobe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donatori_Osobe_OsobaId",
                table: "Donatori");

            migrationBuilder.DropIndex(
                name: "IX_Donatori_OsobaId",
                table: "Donatori");

            migrationBuilder.DropColumn(
                name: "OsobaId",
                table: "Donatori");
        }
    }
}

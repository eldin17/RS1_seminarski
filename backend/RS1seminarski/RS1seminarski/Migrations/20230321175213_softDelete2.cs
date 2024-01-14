using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class softDelete2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Donacije",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Donacije");
        }
    }
}

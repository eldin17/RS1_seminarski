using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1seminarski.Migrations
{
    public partial class VezaKategorijaItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_KategorijaId",
                table: "Items",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Kategorije_KategorijaId",
                table: "Items",
                column: "KategorijaId",
                principalTable: "Kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Kategorije_KategorijaId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_KategorijaId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "Items");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class Pitanje_Zahtev_Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipZivotinje",
                table: "ZAHTEV",
                newName: "Zivotinja");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zivotinja",
                table: "ZAHTEV",
                newName: "TipZivotinje");
        }
    }
}

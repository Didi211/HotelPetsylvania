using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class Pitanje_Zahtev_Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tip zivotinje",
                table: "ZAHTEV",
                newName: "TipZivotinje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipZivotinje",
                table: "ZAHTEV",
                newName: "Tip zivotinje");
        }
    }
}

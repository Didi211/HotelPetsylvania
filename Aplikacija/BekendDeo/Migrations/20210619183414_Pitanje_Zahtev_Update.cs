using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class Pitanje_Zahtev_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImeLjubimca",
                table: "ZAHTEV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tip zivotinje",
                table: "ZAHTEV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Procitano",
                table: "OBAVESTENJE",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImeLjubimca",
                table: "ZAHTEV");

            migrationBuilder.DropColumn(
                name: "Tip zivotinje",
                table: "ZAHTEV");

            migrationBuilder.DropColumn(
                name: "Procitano",
                table: "OBAVESTENJE");
        }
    }
}

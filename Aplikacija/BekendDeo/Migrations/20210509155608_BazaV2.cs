using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class BazaV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Obradjeno",
                table: "ZAHTEV_USLUGA",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Obradjeno",
                table: "ZAHTEV_USLUGA");
        }
    }
}

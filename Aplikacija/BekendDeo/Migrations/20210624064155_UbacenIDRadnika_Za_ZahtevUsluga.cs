using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class UbacenIDRadnika_Za_ZahtevUsluga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RadnikID",
                table: "ZAHTEV_USLUGA",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RadnikID",
                table: "ZAHTEV_USLUGA");
        }
    }
}

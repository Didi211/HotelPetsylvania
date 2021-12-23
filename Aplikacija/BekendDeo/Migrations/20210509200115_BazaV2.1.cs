using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class BazaV21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RADNIK_Username",
                table: "RADNIK",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MUSTERIJA_Username",
                table: "MUSTERIJA",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KORISNIK_Username",
                table: "KORISNIK",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ADMINISTRATOR_Username",
                table: "ADMINISTRATOR",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RADNIK_Username",
                table: "RADNIK");

            migrationBuilder.DropIndex(
                name: "IX_MUSTERIJA_Username",
                table: "MUSTERIJA");

            migrationBuilder.DropIndex(
                name: "IX_KORISNIK_Username",
                table: "KORISNIK");

            migrationBuilder.DropIndex(
                name: "IX_ADMINISTRATOR_Username",
                table: "ADMINISTRATOR");
        }
    }
}

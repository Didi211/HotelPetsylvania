using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class bazaV12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RADNIK_JMBG",
                table: "RADNIK",
                column: "JMBG",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RADNIK_JMBG",
                table: "RADNIK");
        }
    }
}

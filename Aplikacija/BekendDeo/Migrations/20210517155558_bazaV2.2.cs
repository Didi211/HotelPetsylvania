using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class bazaV22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Obradjeno",
                table: "ZAHTEV_USLUGA",
                newName: "Obradjen");

            migrationBuilder.AlterColumn<string>(
                name: "Obradjen",
                table: "ZAHTEV_USLUGA",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Obradjen",
                table: "ZAHTEV",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Obradjen",
                table: "ZAHTEV_USLUGA",
                newName: "Obradjeno");

            migrationBuilder.AlterColumn<bool>(
                name: "Obradjeno",
                table: "ZAHTEV_USLUGA",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Obradjen",
                table: "ZAHTEV",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

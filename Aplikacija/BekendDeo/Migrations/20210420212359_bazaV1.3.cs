using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class bazaV13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RADNIK_ADMINISTRATOR_AdministratorID",
                table: "RADNIK");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorID",
                table: "RADNIK",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RADNIK_ADMINISTRATOR_AdministratorID",
                table: "RADNIK",
                column: "AdministratorID",
                principalTable: "ADMINISTRATOR",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RADNIK_ADMINISTRATOR_AdministratorID",
                table: "RADNIK");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorID",
                table: "RADNIK",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RADNIK_ADMINISTRATOR_AdministratorID",
                table: "RADNIK",
                column: "AdministratorID",
                principalTable: "ADMINISTRATOR",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

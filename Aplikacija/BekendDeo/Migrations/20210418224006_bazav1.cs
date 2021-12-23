using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BekendDeo.Migrations
{
    public partial class bazav1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ADMINISTRATOR",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMINISTRATOR", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "KORISNIK",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zabrana = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KORISNIK", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "MUSTERIJA",
                columns: table => new
                {
                    MusterijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SlikaPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUSTERIJA", x => x.MusterijaID);
                });

            migrationBuilder.CreateTable(
                name: "RADNIK",
                columns: table => new
                {
                    RadnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    DatumPocetkaRada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumPrekidaRada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SlikaPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AdministratorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RADNIK", x => x.RadnikID);
                    table.ForeignKey(
                        name: "FK_RADNIK_ADMINISTRATOR_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "ADMINISTRATOR",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USLUGA",
                columns: table => new
                {
                    UslugaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenaUsluge = table.Column<double>(type: "float", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumDodavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIzmeneCene = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumIzmeneKapaciteta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdministratorAddID = table.Column<int>(type: "int", nullable: false),
                    AdministratorIzmenaCeneID = table.Column<int>(type: "int", nullable: true),
                    AdministratorIzmenaKapacitetaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USLUGA", x => x.UslugaID);
                    table.ForeignKey(
                        name: "FK_USLUGA_ADMINISTRATOR_AdministratorAddID",
                        column: x => x.AdministratorAddID,
                        principalTable: "ADMINISTRATOR",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USLUGA_ADMINISTRATOR_AdministratorIzmenaCeneID",
                        column: x => x.AdministratorIzmenaCeneID,
                        principalTable: "ADMINISTRATOR",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USLUGA_ADMINISTRATOR_AdministratorIzmenaKapacitetaID",
                        column: x => x.AdministratorIzmenaKapacitetaID,
                        principalTable: "ADMINISTRATOR",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RECENZIJA",
                columns: table => new
                {
                    RecenzijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ocena = table.Column<int>(type: "int", nullable: false),
                    DatumPostavljanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MusterijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECENZIJA", x => x.RecenzijaID);
                    table.ForeignKey(
                        name: "FK_RECENZIJA_MUSTERIJA_MusterijaID",
                        column: x => x.MusterijaID,
                        principalTable: "MUSTERIJA",
                        principalColumn: "MusterijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OBAVESTENJE",
                columns: table => new
                {
                    ObavestenjeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BroadCastFlag = table.Column<bool>(type: "bit", nullable: false),
                    KomeNamenjeno = table.Column<int>(type: "int", nullable: true),
                    RadnikID = table.Column<int>(type: "int", nullable: true),
                    AdministratorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBAVESTENJE", x => x.ObavestenjeID);
                    table.ForeignKey(
                        name: "FK_OBAVESTENJE_ADMINISTRATOR_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "ADMINISTRATOR",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OBAVESTENJE_RADNIK_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "RADNIK",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PITANJE",
                columns: table => new
                {
                    PitanjeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TekstPitanja = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TekstOdgovora = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DatumPitanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumOdgovora = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Odgovoreno = table.Column<bool>(type: "bit", nullable: false),
                    MusterijaID = table.Column<int>(type: "int", nullable: false),
                    RadnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PITANJE", x => x.PitanjeID);
                    table.ForeignKey(
                        name: "FK_PITANJE_MUSTERIJA_MusterijaID",
                        column: x => x.MusterijaID,
                        principalTable: "MUSTERIJA",
                        principalColumn: "MusterijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PITANJE_RADNIK_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "RADNIK",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZAHTEV",
                columns: table => new
                {
                    RezervacioniBroj = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cena = table.Column<double>(type: "float", nullable: true),
                    Obradjen = table.Column<bool>(type: "bit", nullable: false),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RadnikID = table.Column<int>(type: "int", nullable: true),
                    MusterijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZAHTEV", x => x.RezervacioniBroj);
                    table.ForeignKey(
                        name: "FK_ZAHTEV_MUSTERIJA_MusterijaID",
                        column: x => x.MusterijaID,
                        principalTable: "MUSTERIJA",
                        principalColumn: "MusterijaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZAHTEV_RADNIK_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "RADNIK",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZAHTEV_USLUGA",
                columns: table => new
                {
                    ZahtevID = table.Column<int>(type: "int", nullable: false),
                    UslugaID = table.Column<int>(type: "int", nullable: false),
                    TipUsluge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Termin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZAHTEV_USLUGA", x => new { x.ZahtevID, x.UslugaID });
                    table.ForeignKey(
                        name: "FK_ZAHTEV_USLUGA_USLUGA_UslugaID",
                        column: x => x.UslugaID,
                        principalTable: "USLUGA",
                        principalColumn: "UslugaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZAHTEV_USLUGA_ZAHTEV_ZahtevID",
                        column: x => x.ZahtevID,
                        principalTable: "ZAHTEV",
                        principalColumn: "RezervacioniBroj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OBAVESTENJE_AdministratorID",
                table: "OBAVESTENJE",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_OBAVESTENJE_RadnikID",
                table: "OBAVESTENJE",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_PITANJE_MusterijaID",
                table: "PITANJE",
                column: "MusterijaID");

            migrationBuilder.CreateIndex(
                name: "IX_PITANJE_RadnikID",
                table: "PITANJE",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_RADNIK_AdministratorID",
                table: "RADNIK",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_RECENZIJA_MusterijaID",
                table: "RECENZIJA",
                column: "MusterijaID");

            migrationBuilder.CreateIndex(
                name: "IX_USLUGA_AdministratorAddID",
                table: "USLUGA",
                column: "AdministratorAddID");

            migrationBuilder.CreateIndex(
                name: "IX_USLUGA_AdministratorIzmenaCeneID",
                table: "USLUGA",
                column: "AdministratorIzmenaCeneID");

            migrationBuilder.CreateIndex(
                name: "IX_USLUGA_AdministratorIzmenaKapacitetaID",
                table: "USLUGA",
                column: "AdministratorIzmenaKapacitetaID");

            migrationBuilder.CreateIndex(
                name: "IX_USLUGA_Naziv",
                table: "USLUGA",
                column: "Naziv",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZAHTEV_MusterijaID",
                table: "ZAHTEV",
                column: "MusterijaID");

            migrationBuilder.CreateIndex(
                name: "IX_ZAHTEV_RadnikID",
                table: "ZAHTEV",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_ZAHTEV_USLUGA_UslugaID",
                table: "ZAHTEV_USLUGA",
                column: "UslugaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KORISNIK");

            migrationBuilder.DropTable(
                name: "OBAVESTENJE");

            migrationBuilder.DropTable(
                name: "PITANJE");

            migrationBuilder.DropTable(
                name: "RECENZIJA");

            migrationBuilder.DropTable(
                name: "ZAHTEV_USLUGA");

            migrationBuilder.DropTable(
                name: "USLUGA");

            migrationBuilder.DropTable(
                name: "ZAHTEV");

            migrationBuilder.DropTable(
                name: "MUSTERIJA");

            migrationBuilder.DropTable(
                name: "RADNIK");

            migrationBuilder.DropTable(
                name: "ADMINISTRATOR");
        }
    }
}

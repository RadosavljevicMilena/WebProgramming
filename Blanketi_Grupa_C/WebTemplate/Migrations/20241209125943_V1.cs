using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bolnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojOdeljenja = table.Column<int>(type: "int", nullable: false),
                    BrojOsoblja = table.Column<int>(type: "int", nullable: false),
                    BrojTelefona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolnice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lekari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDiplomiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDobijanjaLicence = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekari", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentifikacioniBroj = table.Column<int>(type: "int", nullable: false),
                    DatumPotpisivanjaUgovora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Specijalnost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolnicaID = table.Column<int>(type: "int", nullable: true),
                    LekarID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zaposleni_Bolnice_BolnicaID",
                        column: x => x.BolnicaID,
                        principalTable: "Bolnice",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Zaposleni_Lekari_LekarID",
                        column: x => x.LekarID,
                        principalTable: "Lekari",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zaposleni_BolnicaID",
                table: "Zaposleni",
                column: "BolnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposleni_LekarID",
                table: "Zaposleni",
                column: "LekarID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "Bolnice");

            migrationBuilder.DropTable(
                name: "Lekari");
        }
    }
}

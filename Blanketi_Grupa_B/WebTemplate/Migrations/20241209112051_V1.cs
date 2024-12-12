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
                name: "Elektrodistribucije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmailAdresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojRadnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elektrodistribucije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Potrosaci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    GodinaRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MestoRodjenja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potrosaci", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DistributivnaPodrucja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickiBroj = table.Column<int>(type: "int", nullable: false),
                    BrojBrojila = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumPotpisivanjaUgovora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PotrosacDistrPodrucjaID = table.Column<int>(type: "int", nullable: true),
                    ElektrodistribucijeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributivnaPodrucja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DistributivnaPodrucja_Elektrodistribucije_ElektrodistribucijeID",
                        column: x => x.ElektrodistribucijeID,
                        principalTable: "Elektrodistribucije",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DistributivnaPodrucja_Potrosaci_PotrosacDistrPodrucjaID",
                        column: x => x.PotrosacDistrPodrucjaID,
                        principalTable: "Potrosaci",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributivnaPodrucja_ElektrodistribucijeID",
                table: "DistributivnaPodrucja",
                column: "ElektrodistribucijeID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributivnaPodrucja_PotrosacDistrPodrucjaID",
                table: "DistributivnaPodrucja",
                column: "PotrosacDistrPodrucjaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributivnaPodrucja");

            migrationBuilder.DropTable(
                name: "Elektrodistribucije");

            migrationBuilder.DropTable(
                name: "Potrosaci");
        }
    }
}

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
                name: "Gradovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BrojStankovnika = table.Column<int>(type: "int", nullable: false),
                    BrojKoloseka = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vozovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumProizvodnje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxKapacitetPutnika = table.Column<int>(type: "int", nullable: false),
                    MaxTezina = table.Column<double>(type: "float", nullable: false),
                    MaxBrzina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Relacije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojPutnika = table.Column<int>(type: "int", nullable: false),
                    CenaKarte = table.Column<double>(type: "float", nullable: false),
                    DatumSaobracanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradDolaskaID = table.Column<int>(type: "int", nullable: true),
                    GradPolaskaID = table.Column<int>(type: "int", nullable: true),
                    VozSaobracajaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Relacije_Gradovi_GradDolaskaID",
                        column: x => x.GradDolaskaID,
                        principalTable: "Gradovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Relacije_Gradovi_GradPolaskaID",
                        column: x => x.GradPolaskaID,
                        principalTable: "Gradovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Relacije_Vozovi_VozSaobracajaID",
                        column: x => x.VozSaobracajaID,
                        principalTable: "Vozovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacije_GradDolaskaID",
                table: "Relacije",
                column: "GradDolaskaID");

            migrationBuilder.CreateIndex(
                name: "IX_Relacije_GradPolaskaID",
                table: "Relacije",
                column: "GradPolaskaID");

            migrationBuilder.CreateIndex(
                name: "IX_Relacije_VozSaobracajaID",
                table: "Relacije",
                column: "VozSaobracajaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacije");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Vozovi");
        }
    }
}

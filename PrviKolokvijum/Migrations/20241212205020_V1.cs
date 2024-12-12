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
                name: "Akvarijumi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Zapremina = table.Column<double>(type: "float", nullable: false),
                    Temperatura = table.Column<double>(type: "float", nullable: false),
                    DatumPoslednjegCiscenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrekvencijaCiscenja = table.Column<int>(type: "int", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Akvarijumi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ribe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivVrste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Masa = table.Column<double>(type: "float", nullable: false),
                    GodineStarosti = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ribe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dodavanja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumDodavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojJedinkiTeVrste = table.Column<int>(type: "int", nullable: false),
                    AkvarijumID = table.Column<int>(type: "int", nullable: true),
                    RibaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dodavanja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dodavanja_Akvarijumi_AkvarijumID",
                        column: x => x.AkvarijumID,
                        principalTable: "Akvarijumi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Dodavanja_Ribe_RibaID",
                        column: x => x.RibaID,
                        principalTable: "Ribe",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dodavanja_AkvarijumID",
                table: "Dodavanja",
                column: "AkvarijumID");

            migrationBuilder.CreateIndex(
                name: "IX_Dodavanja_RibaID",
                table: "Dodavanja",
                column: "RibaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dodavanja");

            migrationBuilder.DropTable(
                name: "Akvarijumi");

            migrationBuilder.DropTable(
                name: "Ribe");
        }
    }
}

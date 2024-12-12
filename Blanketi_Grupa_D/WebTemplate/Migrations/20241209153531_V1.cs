﻿using System;
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
                name: "Banke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojZaposlenih = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banke", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijenti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojRacuna = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    DatumOtvaranjaRacuna = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DostupnaSredstva = table.Column<double>(type: "float", nullable: false),
                    UkupnoPodignutoDoSada = table.Column<double>(type: "float", nullable: false),
                    KlijentID = table.Column<int>(type: "int", nullable: true),
                    BankaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Racuni_Banke_BankaID",
                        column: x => x.BankaID,
                        principalTable: "Banke",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Racuni_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_BankaID",
                table: "Racuni",
                column: "BankaID");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_KlijentID",
                table: "Racuni",
                column: "KlijentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Racuni");

            migrationBuilder.DropTable(
                name: "Banke");

            migrationBuilder.DropTable(
                name: "Klijenti");
        }
    }
}

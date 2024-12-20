﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTemplate.Models;

#nullable disable

namespace WebTemplate.Migrations
{
    [DbContext(typeof(IspitContext))]
    [Migration("20241209131512_V2")]
    partial class V2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebTemplate.Models.Bolnica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrojOdeljenja")
                        .HasColumnType("int");

                    b.Property<int>("BrojOsoblja")
                        .HasColumnType("int");

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lokacija")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Bolnice");
                });

            modelBuilder.Entity("WebTemplate.Models.Lekar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DatumDiplomiranja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatumDobijanjaLicence")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Lekari");
                });

            modelBuilder.Entity("WebTemplate.Models.Zaposlen", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BolnicaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumPotpisivanjaUgovora")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdentifikacioniBroj")
                        .HasColumnType("int");

                    b.Property<int?>("LekarID")
                        .HasColumnType("int");

                    b.Property<string>("Specijalnost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BolnicaID");

                    b.HasIndex("LekarID");

                    b.ToTable("Zaposleni");
                });

            modelBuilder.Entity("WebTemplate.Models.Zaposlen", b =>
                {
                    b.HasOne("WebTemplate.Models.Bolnica", "Bolnica")
                        .WithMany("ZaposlenjaBolnice")
                        .HasForeignKey("BolnicaID");

                    b.HasOne("WebTemplate.Models.Lekar", "Lekar")
                        .WithMany("ZaposlenjaLekari")
                        .HasForeignKey("LekarID");

                    b.Navigation("Bolnica");

                    b.Navigation("Lekar");
                });

            modelBuilder.Entity("WebTemplate.Models.Bolnica", b =>
                {
                    b.Navigation("ZaposlenjaBolnice");
                });

            modelBuilder.Entity("WebTemplate.Models.Lekar", b =>
                {
                    b.Navigation("ZaposlenjaLekari");
                });
#pragma warning restore 612, 618
        }
    }
}

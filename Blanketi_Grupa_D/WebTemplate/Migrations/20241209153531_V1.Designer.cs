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
    [Migration("20241209153531_V1")]
    partial class V1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebTemplate.Models.Banka", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrojZaposlenih")
                        .HasColumnType("int");

                    b.Property<string>("Lokacija")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("Banke");
                });

            modelBuilder.Entity("WebTemplate.Models.Klijent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Klijenti");
                });

            modelBuilder.Entity("WebTemplate.Models.Racun", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BankaID")
                        .HasColumnType("int");

                    b.Property<string>("BrojRacuna")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<DateTime>("DatumOtvaranjaRacuna")
                        .HasColumnType("datetime2");

                    b.Property<double>("DostupnaSredstva")
                        .HasColumnType("float");

                    b.Property<int?>("KlijentID")
                        .HasColumnType("int");

                    b.Property<double>("UkupnoPodignutoDoSada")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("BankaID");

                    b.HasIndex("KlijentID");

                    b.ToTable("Racuni");
                });

            modelBuilder.Entity("WebTemplate.Models.Racun", b =>
                {
                    b.HasOne("WebTemplate.Models.Banka", "Banka")
                        .WithMany("Racuni")
                        .HasForeignKey("BankaID");

                    b.HasOne("WebTemplate.Models.Klijent", "Klijent")
                        .WithMany("Racuni")
                        .HasForeignKey("KlijentID");

                    b.Navigation("Banka");

                    b.Navigation("Klijent");
                });

            modelBuilder.Entity("WebTemplate.Models.Banka", b =>
                {
                    b.Navigation("Racuni");
                });

            modelBuilder.Entity("WebTemplate.Models.Klijent", b =>
                {
                    b.Navigation("Racuni");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTemplate.Models;

#nullable disable

namespace WebTemplate.Migrations
{
    [DbContext(typeof(IspitContext))]
    partial class IspitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebTemplate.Models.Kuvar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("StrucnaSprema")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Kuvari");
                });

            modelBuilder.Entity("WebTemplate.Models.Restoran", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxBrojGostiju")
                        .HasColumnType("int");

                    b.Property<int>("MaxBrojKuvara")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("Restorani");
                });

            modelBuilder.Entity("WebTemplate.Models.Zaposlen", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("datetime2");

                    b.Property<int?>("KuvarID")
                        .HasColumnType("int");

                    b.Property<int>("Plata")
                        .HasColumnType("int");

                    b.Property<string>("Pozicija")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RestoranID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KuvarID");

                    b.HasIndex("RestoranID");

                    b.ToTable("Zaposleni");
                });

            modelBuilder.Entity("WebTemplate.Models.Zaposlen", b =>
                {
                    b.HasOne("WebTemplate.Models.Kuvar", "Kuvar")
                        .WithMany("Zaposlenja")
                        .HasForeignKey("KuvarID");

                    b.HasOne("WebTemplate.Models.Restoran", "Restoran")
                        .WithMany("Zaposleni")
                        .HasForeignKey("RestoranID");

                    b.Navigation("Kuvar");

                    b.Navigation("Restoran");
                });

            modelBuilder.Entity("WebTemplate.Models.Kuvar", b =>
                {
                    b.Navigation("Zaposlenja");
                });

            modelBuilder.Entity("WebTemplate.Models.Restoran", b =>
                {
                    b.Navigation("Zaposleni");
                });
#pragma warning restore 612, 618
        }
    }
}

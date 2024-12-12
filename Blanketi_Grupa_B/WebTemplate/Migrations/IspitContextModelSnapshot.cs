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

            modelBuilder.Entity("WebTemplate.Models.DistributivnoPodrucje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BrojBrojila")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumPotpisivanjaUgovora")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ElektrodistribucijeID")
                        .HasColumnType("int");

                    b.Property<int>("KorisnickiBroj")
                        .HasColumnType("int");

                    b.Property<int?>("PotrosacDistrPodrucjaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ElektrodistribucijeID");

                    b.HasIndex("PotrosacDistrPodrucjaID");

                    b.ToTable("DistributivnaPodrucja");
                });

            modelBuilder.Entity("WebTemplate.Models.Elektrodistribucija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrojRadnika")
                        .HasColumnType("int");

                    b.Property<string>("EmailAdresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Elektrodistribucije");
                });

            modelBuilder.Entity("WebTemplate.Models.Potrosac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("GodinaRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("MestoRodjenja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Potrosaci");
                });

            modelBuilder.Entity("WebTemplate.Models.DistributivnoPodrucje", b =>
                {
                    b.HasOne("WebTemplate.Models.Elektrodistribucija", "Elektrodistribucije")
                        .WithMany("DistributivnaPodrucje")
                        .HasForeignKey("ElektrodistribucijeID");

                    b.HasOne("WebTemplate.Models.Potrosac", "PotrosacDistrPodrucja")
                        .WithMany("DistributivnaPodrucjaPotrosaca")
                        .HasForeignKey("PotrosacDistrPodrucjaID");

                    b.Navigation("Elektrodistribucije");

                    b.Navigation("PotrosacDistrPodrucja");
                });

            modelBuilder.Entity("WebTemplate.Models.Elektrodistribucija", b =>
                {
                    b.Navigation("DistributivnaPodrucje");
                });

            modelBuilder.Entity("WebTemplate.Models.Potrosac", b =>
                {
                    b.Navigation("DistributivnaPodrucjaPotrosaca");
                });
#pragma warning restore 612, 618
        }
    }
}

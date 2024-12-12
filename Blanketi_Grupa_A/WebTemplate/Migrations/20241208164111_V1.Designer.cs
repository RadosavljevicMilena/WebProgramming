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
    [Migration("20241208164111_V1")]
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

            modelBuilder.Entity("WebTemplate.Models.Aerodrom", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("KapacitetLetelica")
                        .HasColumnType("int");

                    b.Property<int>("KapacitetPutnika")
                        .HasColumnType("int");

                    b.Property<string>("Kod")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<double>("KoordX")
                        .HasColumnType("float");

                    b.Property<double>("KoordY")
                        .HasColumnType("float");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Aerodromi");
                });

            modelBuilder.Entity("WebTemplate.Models.Let", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("AerodromPoletanjaID")
                        .HasColumnType("int");

                    b.Property<int?>("AerodromSletanjaID")
                        .HasColumnType("int");

                    b.Property<int?>("LetelicaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("VremePoletanja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VremeSletanja")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AerodromPoletanjaID");

                    b.HasIndex("AerodromSletanjaID");

                    b.HasIndex("LetelicaID");

                    b.ToTable("Letovi");
                });

            modelBuilder.Entity("WebTemplate.Models.Letelica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrojClanovaPosade")
                        .HasColumnType("int");

                    b.Property<string>("BrojMotora")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KapacitetPutnika")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Letelice");
                });

            modelBuilder.Entity("WebTemplate.Models.Let", b =>
                {
                    b.HasOne("WebTemplate.Models.Aerodrom", "AerodromPoletanja")
                        .WithMany("LetoviPoleteli")
                        .HasForeignKey("AerodromPoletanjaID");

                    b.HasOne("WebTemplate.Models.Aerodrom", "AerodromSletanja")
                        .WithMany("LetoviSleteli")
                        .HasForeignKey("AerodromSletanjaID");

                    b.HasOne("WebTemplate.Models.Letelica", "Letelica")
                        .WithMany("Letovi")
                        .HasForeignKey("LetelicaID");

                    b.Navigation("AerodromPoletanja");

                    b.Navigation("AerodromSletanja");

                    b.Navigation("Letelica");
                });

            modelBuilder.Entity("WebTemplate.Models.Aerodrom", b =>
                {
                    b.Navigation("LetoviPoleteli");

                    b.Navigation("LetoviSleteli");
                });

            modelBuilder.Entity("WebTemplate.Models.Letelica", b =>
                {
                    b.Navigation("Letovi");
                });
#pragma warning restore 612, 618
        }
    }
}
